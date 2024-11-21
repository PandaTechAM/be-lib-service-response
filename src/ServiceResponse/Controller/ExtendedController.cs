using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceResponse.ExceptionHandler;
using ServiceResponse.JsonException;
using ServiceResponse.ServiceResponse;

namespace ServiceResponse.Controller;

[PandaJsonException]
public abstract class ExtendedController : ControllerBase
{
   protected ExtendedController(IExceptionHandler exceptionHandler, ILogger<ExtendedController> logger)
   {
      ExceptionHandler = exceptionHandler;
      Logger = logger;
   }

   private ILogger<ExtendedController> Logger { get; }

   public IExceptionHandler ExceptionHandler { get; set; }

   public T SetResponse<T>(T response) where T : ServiceResponse.ServiceResponse
   {
      Response.StatusCode = (int)response.ResponseStatus;
      response.Success = response.ResponseStatus == ServiceResponseStatus.Ok;
      return response;
   }

   public Task<T> SetResponseAsync<T>(T response) where T : ServiceResponse.ServiceResponse
   {
      Response.StatusCode = (int)response.ResponseStatus;
      response.Success = response.ResponseStatus == ServiceResponseStatus.Ok;
      return Task.FromResult(response);
   }

   protected ServiceResponse.ServiceResponse HandleCall(Action action)
   {
      var response = new ServiceResponse.ServiceResponse();
      try
      {
         action();
      }
      catch (Exception e)
      {
         if (e is ServiceException serviceException)
         {
            Logger?.LogWarning("{Message}", serviceException.Message);
            response = FromException(serviceException);
         }
         else
         {
            Logger?.LogError("{Message}", e);
            response = ExceptionHandler.Handle(new ServiceResponse.ServiceResponse(), e);
         }
      }

      return SetResponse(response);
   }

   protected async Task<ServiceResponse.ServiceResponse> ServiceResponsePaged(Func<Task> func)
   {
      var response = new ServiceResponse.ServiceResponse();
      try
      {
         await func();
      }
      catch (Exception e)
      {
         if (e is ServiceException serviceException)
         {
            Logger.LogWarning("{Message}", serviceException.Message);
            response = FromException(serviceException);
         }
         else
         {
            Logger.LogError("{Message}", e);
            response = ExceptionHandler.Handle(new ServiceResponse.ServiceResponse(), e);
         }
      }

      return await SetResponseAsync(response);
   }

   public static ServiceResponse.ServiceResponse FromException(ServiceException e)
   {
      var response = new ServiceResponse.ServiceResponse
      {
         ResponseStatus = e.ResponseStatus,
         Message = e.Message,
         Success = false
      };

      return response;
   }

   public static ServiceResponse<T> FromException<T>(ServiceException e)
   {
      var response = new ServiceResponse<T>
      {
         ResponseStatus = e.ResponseStatus,
         Message = e.Message,
         Success = false
      };

      return response;
   }

   public static ServiceResponsePaged<T> FromExceptionPaged<T>(ServiceException e)
   {
      var response = new ServiceResponsePaged<T>
      {
         ResponseStatus = e.ResponseStatus,
         Message = e.Message,
         Success = false
      };

      return response;
   }
}