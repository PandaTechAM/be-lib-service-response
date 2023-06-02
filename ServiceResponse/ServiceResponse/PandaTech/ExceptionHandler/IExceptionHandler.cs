namespace PandaTech.ServiceResponse;

public interface IExceptionHandler
{
    T Handle<T>(T serviceResponse, Exception serviceException) where T : ServiceResponse;
}