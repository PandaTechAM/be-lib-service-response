﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceResponseCrafter.JsonException;

public class PandaJsonExceptionAttribute : ActionFilterAttribute
{
   public override void OnActionExecuting(ActionExecutingContext context)
   {
      /*if (!((ExtendedController)context.Controller).ModelState.IsValid)
      {
          // Handle the validation errors and create a custom response object if needed.
          // For example, create a custom dictionary of error messages or modify the structure.
          // Example: var errors = context.Controller.ViewData.ModelState.ToDictionary(...);
          // Example: var customResponse = new { Errors = errors };


          context.Result = new BadRequestObjectResult(new ServiceResponse.ServiceResponse()
              { ResponseStatus = ServiceResponseStatus.BadRequest });
      }*/
   }
}