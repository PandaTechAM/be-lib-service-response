namespace PandaTech.ServiceResponse;

public class ServiceException: Exception
{
    public ServiceResponseStatus ResponseStatus { get; set; } 
    public ServiceException(string text, ServiceResponseStatus responseStatus) : base(text)
    {
        ResponseStatus = responseStatus;
    }
}

