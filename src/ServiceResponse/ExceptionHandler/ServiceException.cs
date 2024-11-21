using ServiceResponse.ServiceResponse;

namespace ServiceResponse.ExceptionHandler;

public class ServiceException : Exception
{
   public ServiceException(string text, ServiceResponseStatus responseStatus) : base(text)
   {
      ResponseStatus = responseStatus;
   }

   public ServiceResponseStatus ResponseStatus { get; set; }
}