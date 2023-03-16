namespace PandaTech.ServiceResponse;

/*
 * This is service response template for .NET 6, 7 web api projects. This template is based on the best practices and
 * has goal to harmonize all API I/O operations. The benefit of this template over other templates is that it totally
 * integrates with OpenAPI and Swagger. So, IActionResults and other services,
 * response will be visible in Swagger UI and loads of other features are and is going to be included.
 * 
 * This Template is designed by PandaTech LLC.
 * We build software with the greatest quality!
 * Our website: www.pandatech.it :)
 */

//See below class for every type of service responses in standardized way:
public class ServiceResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public ServiceResponseStatus ResponseStatus { get; set; } = ServiceResponseStatus.Ok;
}

public class ServiceResponse<T> : ServiceResponse
{
    public T? Data { get; set; }

    public ServiceResponse(T data)
    {
        Data = data;
    }

    public ServiceResponse()
    {
    }
}