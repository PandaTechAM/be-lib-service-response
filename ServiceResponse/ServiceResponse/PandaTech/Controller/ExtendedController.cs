using Microsoft.AspNetCore.Mvc;

namespace PandaTech.ServiceResponse;

public abstract class ExtendedController : ControllerBase
{
    public IExceptionHandler ExceptionHandler { get; set;}

    protected ExtendedController(IExceptionHandler exceptionHandler)
    {
        this.ExceptionHandler = exceptionHandler;
    }
    
    public T SetResponse<T>(T response) where T : ServiceResponse
    {
        Response.StatusCode = response.ResponseStatus switch
        {
            ServiceResponseStatus.Ok => 200,
            ServiceResponseStatus.OkWithNoData => 204,
            ServiceResponseStatus.Moved => 302,
            ServiceResponseStatus.BadRequest => 400,
            ServiceResponseStatus.Duplicate => 400,
            ServiceResponseStatus.Unauthorized => 401,
            ServiceResponseStatus.Forbidden => 403,
            ServiceResponseStatus.NotFound => 404,
            ServiceResponseStatus.Error => 500,
            _ => 500
        };
        return response;
    }
}