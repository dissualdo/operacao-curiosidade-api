using Newtonsoft.Json;
using WebApi.Models.Shared;

namespace WebApi.ApiConfigurations.Handlers
{
    /// <summary>
    /// Provides helper methods to format error response messages into a JSON payload.
    /// </summary>
    public static class FormatResponseMessageHandler
    {
        /// <summary>
        /// Formats a single error message into a JSON payload.
        /// </summary>
        /// <param name="message">The error message to format.</param>
        /// <returns>A JSON string containing the formatted error response.</returns>
        public static string FormatResponseMessage(string message)
        {
            var errors = new List<string> { message };
            var payload = BuildResponse(errors);
            return SerializeResponse(payload);
        }

        /// <summary>
        /// Formats multiple error messages into a JSON payload.
        /// </summary>
        /// <param name="messages">A list of error messages to format.</param>
        /// <returns>A JSON string containing the formatted error response.</returns>
        public static string FormatResponseMessage(List<string> messages)
        {
            var payload = BuildResponse(messages);
            return SerializeResponse(payload);
        }

        /// <summary>
        /// Builds an <see cref="ErrorModel"/> from a list of error messages.
        /// </summary>
        /// <param name="errors">The list of error messages.</param>
        /// <returns>An <see cref="ErrorModel"/> populated with the provided errors.</returns>
        public static ErrorModel BuildResponse(List<string> errors) => new ErrorModel
        {
            Errors = errors
        };

        /// <summary>
        /// Builds an <see cref="ErrorModel"/> from a single error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>An <see cref="ErrorModel"/> populated with the provided error message.</returns>
        public static ErrorModel BuildResponse(string message)
        {
            var errors = new List<string> { message };
            return new ErrorModel
            {
                Errors = errors
            };
        }

        /// <summary>
        /// Serializes an <see cref="ErrorModel"/> into a JSON string.
        /// </summary>
        /// <param name="error">The <see cref="ErrorModel"/> to serialize.</param>
        /// <returns>A JSON string representing the error model.</returns>
        private static string SerializeResponse(ErrorModel error) => JsonConvert.SerializeObject(error);
    }
}
