using Microsoft.AspNetCore.Mvc;

namespace PandaTech.ServiceResponse;

public abstract class ExtendedController : ControllerBase
{
    public IExceptionHandler ExceptionHandler { get; set; }

    protected ExtendedController(IExceptionHandler exceptionHandler)
    {
        ExceptionHandler = exceptionHandler;
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