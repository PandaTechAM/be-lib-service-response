namespace PandaTech.ServiceResponse;

public class PublicExceptionHandler : IExceptionHandler
{
    public ServiceResponse Handle(ServiceResponse serviceResponse, Exception serviceException)
    {
        serviceResponse.Success = false;
        serviceResponse.Message = serviceException.InnerException?.Message ?? serviceException.Message;
        serviceResponse.ResponseStatus = ServiceResponseStatus.Error;
        return serviceResponse;
    }
}