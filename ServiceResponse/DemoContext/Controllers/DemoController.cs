using Microsoft.AspNetCore.Mvc;
using PandaTech.ServiceResponse;

namespace DemoContext.Controllers;

[Controller]
[Route("[controller]")]
[ServiceValidationFilter]
[ServiceExceptionFilter]
public class DemoController : ExtendedController
{
    public DemoController(IExceptionHandler exceptionHandler, ILogger<DemoController> logger) : base(exceptionHandler,
        logger)
    {
    }

    [HttpPost]
    public ServiceResponse PostServiceResponse([FromBody] SomeDTO dto)
    {
        return new();
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
        => new() { ResponseStatus = responseStatus };


    [HttpPost("debug2")]
    public Task<ServiceResponse<SomeDTO>> GetSomeDto([FromBody] SomeDTO dto)
        => Task.FromResult<ServiceResponse<SomeDTO>>(new(dto));

    [HttpGet("debug3")]
    public ServiceResponse ThrowServiceException()
        => throw new ServiceException("Test exception", ServiceResponseStatus.Moved);


    [HttpGet("debug5")]
    public ServiceResponse ThrowException()
        => throw new Exception("Test exception");
}

public class SomeDTO
{
    public string Name { get; set; }
    public int Age { get; set; }
}