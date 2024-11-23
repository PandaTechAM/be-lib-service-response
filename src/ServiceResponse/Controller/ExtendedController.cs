using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceResponse.Dtos;
using ServiceResponse.ExceptionHandler;
using ServiceResponse.JsonException;

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

   public T SetResponse<T>(T response) where T : Dtos.ServiceResponse
   {
      Response.StatusCode = (int)response.ResponseStatus;
      response.Success = response.ResponseStatus == ServiceResponseStatus.Ok;
      return response;
   }

   public Task<T> SetResponseAsync<T>(T response) where T : Dtos.ServiceResponse
   {
      Response.StatusCode = (int)response.ResponseStatus;
      response.Success = response.ResponseStatus == ServiceResponseStatus.Ok;
      return Task.FromResult(response);
   }

   protected Dtos.ServiceResponse HandleCall(Action action)
   {
      var response = new Dtos.ServiceResponse();
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
            response = ExceptionHandler.Handle(new Dtos.ServiceResponse(), e);
         }
      }

      return SetResponse(response);
   }

   protected async Task<Dtos.ServiceResponse> ServiceResponsePaged(Func<Task> func)
   {
      var response = new Dtos.ServiceResponse();
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
            response = ExceptionHandler.Handle(new Dtos.ServiceResponse(), e);
         }
      }

      return await SetResponseAsync(response);
   }

   public static Dtos.ServiceResponse FromException(ServiceException e)
   {
      var response = new Dtos.ServiceResponse
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