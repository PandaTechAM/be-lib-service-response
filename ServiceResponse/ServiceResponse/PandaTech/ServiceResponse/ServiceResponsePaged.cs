namespace PandaTech.ServiceResponse;

public class ServiceResponsePaged<T> : ServiceResponse
{
    public ResponseDataPaged<T> ResponseData { get; set; } = new();

    public ServiceResponsePaged(List<T> data, int page, int pageSize, int totalCount) : base()
    {
        ResponseData = new ResponseDataPaged<T>(data, page, pageSize, totalCount);
    }
    
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
    
    public ResponseDataPaged(List<T> data, int page, int pageSize, int totalCount)
    {
        Data = data;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public ResponseDataPaged()
    {
    }
}