namespace PandaTech.ServiceResponse;

public class ServiceResponsePaged<T> : ServiceResponse<T>
{
    public ResponseDataPaged<T>? ResponseData { get; set; }

    public ServiceResponsePaged(ResponseDataPaged<T> data) : base(data)
    {
        ResponseData = data;
    }

    public ServiceResponsePaged()
    {
    }
}

public class ResponseDataPaged<T> : ResponseData<T>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public int TotalCount { get; set; } = 0;
}