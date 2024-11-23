using ServiceResponseCrafter.Dtos;

namespace ServiceResponseCrafter.ExceptionHandler;

public class PublicExceptionHandler : IExceptionHandler
{
   public T Handle<T>(T serviceResponse, Exception serviceException) where T : Dtos.ServiceResponse
   {
      if (serviceException is ServiceException exception)
      {
         serviceResponse.Message = exception.Message;
         serviceResponse.ResponseStatus = exception.ResponseStatus;
         serviceResponse.Success = false;
         return serviceResponse;
      }

      serviceResponse.Success = false;
      serviceResponse.Message = "An error occurred, please contact support.";
      serviceResponse.ResponseStatus = ServiceResponseStatus.Error;
      return serviceResponse;
   }
}