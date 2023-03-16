using System.Text;

namespace PandaTech.ServiceResponse;

public class DebugExceptionHandler : IExceptionHandler
{
    public ServiceResponse Handle(ServiceResponse serviceResponse, Exception serviceException)
    {
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