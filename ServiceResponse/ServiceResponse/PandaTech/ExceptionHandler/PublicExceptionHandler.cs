namespace PandaTech.ServiceResponse;

public class PublicExceptionHandler : IExceptionHandler
{
    public T Handle<T>(T serviceResponse, Exception serviceException) where T : IServiceResponse
    {
        serviceResponse.Success = false;
        serviceResponse.Message = "An error occurred, please contact support.";
        serviceResponse.ResponseStatus = ServiceResponseStatus.Error;
        return serviceResponse;
    }
}