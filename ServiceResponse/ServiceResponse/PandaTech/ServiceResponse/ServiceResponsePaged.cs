namespace PandaTech.ServiceResponse;

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

    public ServiceResponsePaged()
    {
    }
}