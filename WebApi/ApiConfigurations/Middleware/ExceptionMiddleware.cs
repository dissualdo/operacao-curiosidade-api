using System.Net;
using WebApi.Models.Shared;
using System.Diagnostics.CodeAnalysis;
using WebApi.ApiConfigurations.Handlers;

namespace WebApi.ApiConfigurations.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally in the application.
    /// Catches exceptions thrown during the request processing pipeline and returns a formatted JSON response.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger to log exception details.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware to process HTTP requests and handle any exceptions that occur.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var responseMessage = string.Empty;
            try
            {
                // Proceed with the next middleware component in the pipeline.
                await _next(httpContext);
            }
            catch (AppException ex)
            {
                // Handle custom application exceptions.
                _logger.LogError($"[Global Exception Middleware]: {ex}");
                responseMessage = FormatResponseMessageHandler.FormatResponseMessage(ex.Message);
                await HandleExceptionAsync(httpContext, responseMessage, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                // Handle argument exceptions.
                _logger.LogError($"[Global Exception Middleware]: {ex}");
                responseMessage = FormatResponseMessageHandler.FormatResponseMessage(ex.Message);
                await HandleExceptionAsync(httpContext, responseMessage, HttpStatusCode.BadRequest).ConfigureAwait(false);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle unauthorized access exceptions.
                _logger.LogError($"[Global Exception Middleware]: {ex}");
                responseMessage = FormatResponseMessageHandler.FormatResponseMessage(ex.Message);
                await HandleExceptionAsync(httpContext, responseMessage, HttpStatusCode.Unauthorized).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Handle all other exceptions.
                _logger.LogError($"[Global Exception Middleware]: {ex}");
                responseMessage = FormatResponseMessageHandler.FormatResponseMessage(ex.Message);
                _logger.LogError(ex.ToString());
                await HandleExceptionAsync(httpContext, responseMessage, HttpStatusCode.InternalServerError).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Writes the error response to the HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context to write the response to.</param>
        /// <param name="payload">The JSON-formatted error payload.</param>
        /// <param name="statusCodes">The HTTP status code to return.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static Task HandleExceptionAsync(HttpContext context, string payload, HttpStatusCode statusCodes)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCodes;
            return context.Response.WriteAsync(payload);
        }
    }
}
