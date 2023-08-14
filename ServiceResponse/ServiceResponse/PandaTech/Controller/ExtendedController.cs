using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PandaTech.JsonException;

namespace PandaTech.ServiceResponse;

[PandaJsonException]
public abstract class ExtendedController : ControllerBase
{
    ILogger<ExtendedController> Logger { get; set; }
    
    public IExceptionHandler ExceptionHandler { get; set; }

    protected ExtendedController(IExceptionHandler exceptionHandler, ILogger<ExtendedController> logger)
    {
        ExceptionHandler = exceptionHandler;
        Logger = logger;
    }

    public T SetResponse<T>(T response) where T : ServiceResponse
    {
        Response.StatusCode = (int)response.ResponseStatus;
        response.Success = response.ResponseStatus == ServiceResponseStatus.Ok;
        return response;
    }

    public Task<T> SetResponseAsync<T>(T response) where T : ServiceResponse
    {
        Response.StatusCode = (int)response.ResponseStatus;
        response.Success = response.ResponseStatus == ServiceResponseStatus.Ok;
        return Task.FromResult(response);
    }

    public ServiceResponse HandleCall(Action action)
    {
        var response = new ServiceResponse();
        try
        {
            action();
        }
        catch (Exception e)
        {
            if (e is ServiceException serviceException)
            {
                Logger?.LogWarning("{Message}", serviceException.Message);
                response = FromException(serviceException);
            }
            else
            {
                Logger?.LogError("{Message}", e);
                response = ExceptionHandler.Handle(new ServiceResponse(), e);
            }
        }

        return SetResponse(response);
    }
    public ServiceResponse<T> HandleCall<T>(Func<T> func)
    {
        ServiceResponse<T>  response;
        try
        {
            response = new ServiceResponse<T>(func()) ;
        }
        catch (Exception e)
        {
            if (e is ServiceException serviceException)
            {
                Logger?.LogWarning("{Message}", serviceException.Message);
                response = FromException<T>(serviceException);
            }
            else
            {
                Logger?.LogError("{Message}", e);
                response = ExceptionHandler.Handle(new ServiceResponse<T>(), e);
            }
        }

        return SetResponse(response);
    }

    public ServiceResponsePaged<T> HandleCall<T>(Func<ServiceResponsePaged<T>> func)
    {
        ServiceResponsePaged<T>  response;
        try
        {
            response = func();
        }
        catch (Exception e)
        {
            if (e is ServiceException serviceException)
            {
                Logger?.LogWarning("{Message}", serviceException.Message);
                response = FromExceptionPaged<T>(serviceException);
            }
            else
            {
                Logger?.LogError("{Message}", e);
                response = ExceptionHandler.Handle(new ServiceResponsePaged<T>(), e);
            }
        }

        return SetResponse(response);
    }

    public static ServiceResponse FromException(ServiceException e)
    {
        var response = new ServiceResponse
        {
            ResponseStatus = e.ResponseStatus,
            Message = e.Message,
            Success = false
        };

        return response;
    }

    public static ServiceResponse<T> FromException<T>(ServiceException e)
    {
        var response = new ServiceResponse<T>
        {
            ResponseStatus = e.ResponseStatus,
            Message = e.Message,
            Success = false
        };

        return response;
    }

    public static ServiceResponsePaged<T> FromExceptionPaged<T>(ServiceException e)
    {
        var response = new ServiceResponsePaged<T>
        {
            ResponseStatus = e.ResponseStatus,
            Message = e.Message,
            Success = false
        };

        return response;
    }
}