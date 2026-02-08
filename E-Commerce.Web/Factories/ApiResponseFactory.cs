using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var Errors = context.ModelState.Where(a => a.Value.Errors.Any())
                   .Select(a => new ValidationError()
                   {
                       Field = a.Key,
                       Errors = a.Value.Errors.Select(a => a.ErrorMessage)
                   });
            var Response = new ValidationErrorToReturn()
            {
                validationErrors = Errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
