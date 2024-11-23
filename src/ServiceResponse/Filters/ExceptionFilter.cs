using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceResponse.Controller;
using ServiceResponse.ExceptionHandler;

namespace ServiceResponse.Filters;

public class ServiceExceptionFilterAttribute : ExceptionFilterAttribute
{
   public override void OnException(ExceptionContext context)
   {
      var exceptionHandler =
         context.HttpContext.RequestServices.GetRequiredService<IExceptionHandler>();
      var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ServiceExceptionFilterAttribute>>();
      var exception = context.Exception;
      Dtos.ServiceResponse serviceResponse;

      if (exception is ServiceException serviceException)
      {
         logger.LogWarning("{Message}", serviceException.Message);
         serviceResponse = ExtendedController.FromException(serviceException);
         context.Result = new ObjectResult(serviceResponse)
         {
            StatusCode = (int)serviceResponse.ResponseStatus
         };
      }
      else
      {
         logger.LogError("{Message}", exception);
         serviceResponse = exceptionHandler.Handle(new Dtos.ServiceResponse(), exception);

         context.Result = new ObjectResult(serviceResponse)
         {
            StatusCode = 500
         };
      }
   }
}