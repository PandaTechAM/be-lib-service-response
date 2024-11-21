namespace ServiceResponse.ServiceResponse;

public class ServiceResponsePaged<T> : ServiceResponse
{
   public ServiceResponsePaged(List<T> data,
      int page,
      int pageSize,
      int totalCount,
      Dictionary<string, object?>? aggregates = null)
   {
      ResponseData = new ResponseDataPaged<T>(data, page, pageSize, totalCount, aggregates);
   }

   public ServiceResponsePaged(ResponseDataPaged<T> data)
   {
      ResponseData = data;
   }

   public ServiceResponsePaged()
   {
   }

   public ResponseDataPaged<T> ResponseData { get; set; } = new();
}

public class ResponseDataPaged<T>
{
   public ResponseDataPaged(List<T> data,
      int page,
      int pageSize,
      long totalCount,
      Dictionary<string, object?>? aggregates)
   {
      Data = data;
      Page = page;
      PageSize = pageSize;
      TotalCount = totalCount;
      Aggregates = aggregates;
   }

   public ResponseDataPaged()
   {
   }

   public List<T> Data { get; set; } = new();
   public int Page { get; set; } = 1;
   public int PageSize { get; set; } = 20;
   public long TotalCount { get; set; }
   public Dictionary<string, object?>? Aggregates { get; set; }
}