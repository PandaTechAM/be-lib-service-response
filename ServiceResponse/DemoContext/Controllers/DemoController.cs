using Microsoft.AspNetCore.Mvc;
using PandaTech.ServiceResponse;

namespace DemoContext.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController: ExtendedController
{
    public DemoController(IExceptionHandler exceptionHandler) : base(exceptionHandler)
    {
    }
    
    [HttpGet]
    public ServiceResponse GetServiceResponse([FromQuery] ServiceResponseStatus responseStatus)
    {
        var response = new ServiceResponse();
        try
        {
            response.ResponseStatus = responseStatus;
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle(response, e);            
        }
        return SetResponse(response);
    }
    
    [HttpGet("debug")]
    public ServiceResponsePaged<int> GetServiceResponsePaged([FromQuery] ServiceResponseStatus responseStatus)
    {
        var response = new ServiceResponsePaged<int>();
        try
        {
            response.ResponseStatus = responseStatus;
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle(response, e);            
        }
        return SetResponse(response);
    }
}