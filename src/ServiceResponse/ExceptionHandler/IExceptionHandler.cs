namespace ServiceResponseCrafter.ExceptionHandler;

public interface IExceptionHandler
{
   T Handle<T>(T serviceResponse, Exception serviceException) where T : Dtos.ServiceResponse;
}