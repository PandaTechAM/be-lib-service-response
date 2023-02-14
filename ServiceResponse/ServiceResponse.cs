using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceResponse;

/*
 * This is service response template for .NET 6, 7 web api projects. This template is based on the best practices and
 * has goal to harmonize all API I/O operations. The benefit of this template over other templates is that it totally
 * integrates with OpenAPI and Swagger. So, IActionResults and other services,
 * response will be visible in Swagger UI and loads of other features are and is going to be included.
 * 
 * This Template is designed by PandaTech LLC.
 * We build software with the greatest quality!
 * Our link: www.pandatech.it :)
 */

//See below class for every type of service responses in standatized way:
public class ServiceResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public ServiceResponseStatus ResponseStatus { get; set; } = ServiceResponseStatus.Ok;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServiceResponseStatus
{
    Ok, //Post/Put request succeeded.
    Moved, //Requested resource assigned to new temp/perm URL
    BadRequest, //Client side error due to some reason
    Duplicate, //Duplicative data
    Unauthorized, //Client is not authenticated, unknown client
    Forbidden, //Client is known but access is restricted
    NotFound, //Endpoint is valid but the resource itself does not exist.
    Error //Other error
}
public class ServiceResponse<T> : ServiceResponse
{
    public T? Data { get; set; }

    public ServiceResponse(T data)
    {
        Data = data;
    }

    public ServiceResponse() { }
}
public class ServiceResponsePaged<T> : ServiceResponse<T>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public int TotalCount { get; set; }

    public ServiceResponsePaged(T data, int page, int pageSize, int totalCount) : base(data)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public ServiceResponsePaged() { }
}

public class ExtendedController : ControllerBase
{
    public void ExceptionHandler(ServiceResponse serviceResponse, Exception serviceException)
    {
        serviceResponse.Success = false;
        serviceResponse.Message = serviceException.InnerException?.Message ?? serviceException.Message;
        serviceResponse.ResponseStatus = ServiceResponseStatus.Error;
    }

    public T SetResponse<T>(T response) where T : ServiceResponse
    {
        Response.StatusCode = response.ResponseStatus switch
        {
            ServiceResponseStatus.Ok => 200,
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