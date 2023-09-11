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

    public  T SetResponse<T>(T response) where T : ServiceResponse
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



    protected async Task<ServiceResponse> ServiceResponsePaged(Func<Task> func)
    {
        var response = new ServiceResponse();
        try
        {
            await func();
        }
        catch (Exception e)
        {
            if (e is ServiceException serviceException)
            {
                Logger.LogWarning("{Message}", serviceException.Message);
                response = FromException(serviceException);
            }
            else
            {
                Logger.LogError("{Message}", e);
                response = ExceptionHandler.Handle(new ServiceResponse(), e);
            }
        }

        return await SetResponseAsync(response);
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