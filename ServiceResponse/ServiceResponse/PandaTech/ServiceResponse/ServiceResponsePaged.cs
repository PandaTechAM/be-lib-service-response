namespace PandaTech.ServiceResponse;

public class ServiceResponsePaged<T> : ServiceResponse
{
    public ResponseDataPaged<T> ResponseData { get; set; } = new();

    public ServiceResponsePaged(ResponseDataPaged<T> data) : base()
    {
        ResponseData = data;
    }

    public ServiceResponsePaged()
    {
    }
}

public class ResponseDataPaged<T>
{
    public List<T> Data { get; set; } = new();
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public int TotalCount { get; set; } = 0;
}