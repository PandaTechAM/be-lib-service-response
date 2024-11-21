namespace ServiceResponse.ExceptionHandler;

public interface IExceptionHandler
{
   T Handle<T>(T serviceResponse, Exception serviceException) where T : ServiceResponse.ServiceResponse;
}