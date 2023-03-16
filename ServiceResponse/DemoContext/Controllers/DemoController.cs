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
        var response = new ServiceResponse
        {
            ResponseStatus = responseStatus
        };
        return SetResponse(response);
    }
}