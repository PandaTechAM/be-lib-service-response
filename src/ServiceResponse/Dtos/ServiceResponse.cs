namespace ServiceResponseCrafter.Dtos;

public class ServiceResponse
{
   public bool Success { get; set; } = true;
   public string Message { get; set; } = string.Empty;
   public ServiceResponseStatus ResponseStatus { get; set; } = ServiceResponseStatus.Ok;
}

public class ServiceResponse<T> : ServiceResponse
{
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
      ResponseData = new ResponseData<T>();
   }

   public ResponseData<T> ResponseData { get; set; } = new();
}

public class ResponseData<T>
{
   public ResponseData(T? data)
   {
      Data = data;
   }

   public ResponseData()
   {
      Data = default;
   }

   public T? Data { get; set; }
}