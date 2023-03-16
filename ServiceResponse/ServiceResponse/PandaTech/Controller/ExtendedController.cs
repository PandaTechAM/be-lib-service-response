using Microsoft.AspNetCore.Mvc;

namespace PandaTech.ServiceResponse;

public abstract class ExtendedController : ControllerBase
{
    public IExceptionHandler ExceptionHandler { get; set;}

    protected ExtendedController(IExceptionHandler exceptionHandler)
    {
        ExceptionHandler = exceptionHandler;
    }
    
    public T SetResponse<T>(T response) where T : ServiceResponse
    {
        Response.StatusCode = (int)response.ResponseStatus;
        return response;
    }
}