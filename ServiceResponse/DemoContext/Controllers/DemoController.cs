using Microsoft.AspNetCore.Mvc;
using PandaTech.JsonException;
using PandaTech.ServiceResponse;

namespace DemoContext.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ExtendedController
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

    [HttpGet("async")]
    public async Task<ServiceResponse> TestAsync()
    {
        var response = new ServiceResponse();
        try
        {
            response.ResponseStatus = ServiceResponseStatus.Ok;
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle(response, e);
        }

        return await SetResponseAsync(response);
    }


    [HttpGet("debug")]
    public ServiceResponsePaged<int> GetServiceResponsePaged([FromQuery] ServiceResponseStatus responseStatus)
    => HandleCall(() => new ServiceResponsePaged<int> {ResponseStatus = responseStatus});
    
    /*{
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
    }*/
    
    [HttpPost("debug2")]

    public Task<ServiceResponse<SomeDTO>> GetSomeDto([FromBody] SomeDTO dto)
        => Task.FromResult<ServiceResponse<SomeDTO>>(new(dto));


}

public class SomeDTO
{
    public string Name { get; set; }
    public int Age { get; set; }
}