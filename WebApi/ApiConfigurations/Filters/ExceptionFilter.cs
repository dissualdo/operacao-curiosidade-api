using System.Net;
using WebApi.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.ApiConfigurations.Filters
{
    /// <summary>
    /// A custom exception filter that catches unhandled exceptions and returns a standardized error response.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs in the application.
        /// </summary>
        /// <param name="context">The exception context.</param>
        public void OnException(ExceptionContext context)
        {
            var errors = new List<string>();

            if ((context.Exception.InnerException ?? context.Exception) is AppException exception)
            {
                if (!string.IsNullOrWhiteSpace(exception.Message))
                    errors.Add(exception.Message);

                if (!string.IsNullOrWhiteSpace(exception.InnerException?.Message))
                    errors.Add(exception.InnerException.Message);

                var response = new
                {
                    code = exception.Code ?? "ApplicationError",
                    type = exception.Type ?? "toast",
                    error = new { messages = errors }
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {

                errors.Add("An unexpected error occurred. Please try again later or contact support.");

                var response = new
                {
                    code = "InternalServerError",
                    type = "toast",
                    error = new { messages = errors }
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
