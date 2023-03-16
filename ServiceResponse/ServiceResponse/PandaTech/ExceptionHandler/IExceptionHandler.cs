namespace PandaTech.ServiceResponse;

public interface IExceptionHandler
{
    ServiceResponse Handle(ServiceResponse serviceResponse, Exception serviceException);
}