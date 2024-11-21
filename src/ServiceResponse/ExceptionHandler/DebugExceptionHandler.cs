using System.Text;
using ServiceResponse.ServiceResponse;

namespace ServiceResponse.ExceptionHandler;

public class DebugExceptionHandler : IExceptionHandler
{
   public T Handle<T>(T serviceResponse, Exception serviceException) where T : ServiceResponse.ServiceResponse
   {
      if (serviceException is ServiceException exception)
      {
         serviceResponse.Message = exception.Message;
         serviceResponse.ResponseStatus = exception.ResponseStatus;
         serviceResponse.Success = false;
         return serviceResponse;
      }

      var builder = new StringBuilder();

      builder.AppendLine(serviceException.Message);
      builder.AppendLine(serviceException.StackTrace);
      if (serviceException.InnerException != null)
      {
         builder.AppendLine(serviceException.InnerException?.Message);
         builder.AppendLine(serviceException.InnerException?.StackTrace);
      }

      serviceResponse.Success = false;
      serviceResponse.Message = builder.ToString();
      serviceResponse.ResponseStatus = ServiceResponseStatus.Error;
      return serviceResponse;
   }
}