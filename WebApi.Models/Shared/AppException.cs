namespace WebApi.Models.Shared
{
    /// <summary>
    /// Custom exception class for handling application-specific errors.
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// Unique error code associated with the exception.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Type of the exception, defaulting to "toast".
        /// This can be used to categorize how the error should be displayed.
        /// </summary>
        public string Type { get; set; } = "toast";

        /// <summary>
        /// Service name where the exception occurred.
        /// Defaults to "web-api".
        /// </summary>
        public string Service { get; set; } = "web-api";

        /// <summary>
        /// The date and time when the exception occurred, formatted as "dd/MM/yyyy hh:mm:ss".
        /// </summary>
        public string ErrorDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

        #region .: Constructor :.

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        public AppException() {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        public AppException(string message) : base(message)
        {
            Code = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class with a specified error code and message.
        /// </summary>
        /// <param name="code">The error code associated with the exception.</param>
        /// <param name="message">The error message that describes the exception.</param>
        public AppException(string code, string message) : base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class with a list of error messages.
        /// </summary>
        /// <param name="message">The list of error messages.</param>
        public AppException(List<string> message) : base(message.ToString()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class with a specified error message and inner exception.
        /// </summary>
        /// <param name="message">The error message that describes the exception.</param>
        /// <param name="innerException">The inner exception that caused this exception.</param>
        public AppException(string message, Exception innerException) : base(message, innerException) { }
        #endregion

        #region .: User :.
        /// <summary>
        /// Error message for user not found.
        /// </summary>
        public const string UserNotRegistered = "Usuário não encontratado";

        /// <summary>
        /// Error message for user not found.
        /// </summary>
        public const string LoginNotRegistered = "Login ou Senha Inválidos";
        #endregion
    }
}
