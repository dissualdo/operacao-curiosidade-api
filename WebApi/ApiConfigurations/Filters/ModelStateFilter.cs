using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using WebApi.ApiConfigurations.Handlers;

namespace WebApi.ApiConfigurations.Filters
{
    /// <summary>
    /// An action filter that validates the model state before the action method executes.
    /// If the model state is invalid, it returns a formatted error response and cancels the action execution.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ModelStateFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Called asynchronously before the action executes.
        /// Checks if the model state is valid; if not, returns a JSON formatted error response with HTTP 400.
        /// Otherwise, the request is passed on to the next delegate in the pipeline.
        /// </summary>
        /// <param name="context">The context for the action execution.</param>
        /// <param name="next">The delegate to execute the next action filter or the action itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                // Extract error messages from the ModelState.
                var messages = context.ModelState.Values
                                    .Where(x => x.Errors.Count > 0)
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage)
                                    .ToList();

                // Format the error messages using the custom response handler.
                var response = FormatResponseMessageHandler.FormatResponseMessage(messages);

                // Return the response as a ContentResult with a 400 Bad Request status.
                context.Result = new ContentResult
                {
                    Content = response,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
            else
            {
                // Proceed with the next action filter or the action if ModelState is valid.
                await next();
            }
        }
    }
}