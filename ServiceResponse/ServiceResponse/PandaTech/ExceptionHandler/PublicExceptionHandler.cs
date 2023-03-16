namespace PandaTech.ServiceResponse;

public class PublicExceptionHandler : IExceptionHandler
{
    public ServiceResponse Handle(ServiceResponse serviceResponse, Exception serviceException)
    {
        serviceResponse.Success = false;
        serviceResponse.Message = "An error occurred, please contact support.";
        serviceResponse.ResponseStatus = ServiceResponseStatus.Error;
        return serviceResponse;
    }
}