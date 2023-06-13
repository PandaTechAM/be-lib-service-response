namespace PandaTech.ServiceResponse;


public class ServiceResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public ServiceResponseStatus ResponseStatus { get; set; } = ServiceResponseStatus.Ok;
}

public class ServiceResponse<T> : ServiceResponse
{
    public ResponseData<T> ResponseData { get; set; } = new ();

    public ServiceResponse(ResponseData<T> data)
    {
        ResponseData = data;
    }
    
    public ServiceResponse(T data)
    {
        ResponseData = new ResponseData<T>(data);
    }

    public ServiceResponse()
    {
    }
}

public class ResponseData<T>
{
    public T? Data { get; set; }
    
    public ResponseData(T? data)
    {
        Data = data;
    }
    
    public ResponseData()
    {
    }
}
