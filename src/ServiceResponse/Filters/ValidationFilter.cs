using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceResponseCrafter.Dtos;

namespace ServiceResponseCrafter.Filters;

public class ServiceValidationFilterAttribute : ActionFilterAttribute
{
   public override void OnActionExecuting(ActionExecutingContext context)
   {
      Console.WriteLine("OnActionExecuting");
      if (context.ModelState.IsValid)
      {
         return;
      }

      var errorsInModelState = context.ModelState
                                      .Where(x => x.Value!.Errors.Count > 0)
                                      .ToDictionary(kvp => kvp.Key,
                                         kvp => kvp.Value!.Errors.Select(x => x.ErrorMessage))
                                      .ToArray();

      var errorResponse = new Dtos.ServiceResponse
      {
         ResponseStatus = ServiceResponseStatus.BadRequest
      };

      var errors = new StringBuilder();

      foreach (var error in errorsInModelState)
      {
         foreach (var subError in error.Value)
         {
            errors.Append($"{error.Key}: {subError} \n");
         }
      }

      errorResponse.Message = errors.ToString();
      context.Result = new BadRequestObjectResult(errorResponse);
   }
}