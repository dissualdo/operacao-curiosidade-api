using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Mime;
using WebApi.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.ApiConfigurations.Filters
{
    /// <summary>
    /// An action filter that validates the model state before an action method is executed.
    /// If the model state is invalid, the filter returns a <see cref="BadRequestObjectResult"/> with error details.
    /// </summary>
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Called before the action method is executed.
        /// Validates the model state and, if invalid, short-circuits the request by returning a 400 Bad Request response.
        /// </summary>
        /// <param name="context">The context for the action executing.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Extract error messages from the ModelState.
                var errors = context.ModelState.Values
                    .Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                // Create a BadRequestObjectResult with the error details wrapped in an AppException.
                context.Result = new BadRequestObjectResult(new AppException(errors));

                // Set the HTTP response status code and content type.
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            }
        }
    }
}
