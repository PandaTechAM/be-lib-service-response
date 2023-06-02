namespace PandaTech.ServiceResponse;


public class ServiceResponse : IServiceResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public ServiceResponseStatus ResponseStatus { get; set; } = ServiceResponseStatus.Ok;
}

public interface IServiceResponse
{
    bool Success { get; set; }
    string Message { get; set; }
    ServiceResponseStatus ResponseStatus { get; set; }
}

public class ServiceResponse<T> : ServiceResponse
{
    public ResponseData<T>? ResponseData { get; set; }

    public ServiceResponse(ResponseData<T> data)
    {
        ResponseData = data;
    }

    public ServiceResponse()
    {
    }
}

public class ResponseData<T>
{
    public T? Data { get; set; }
}
