namespace WebApi.Models.Shared
{
    /// <summary>
    /// Represents a standardized error model for returning error information from the API.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Gets or sets the label type associated with the error message.
        /// This can be used to identify the category or severity of the error.
        /// </summary>
        public string Labeltype { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a collection of error messages.
        /// This list contains detailed error descriptions.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the error display type.
        /// Defaults to "toast", which can be used by front-end components for displaying error notifications.
        /// </summary>
        public string Type { get; set; } = "toast";

        /// <summary>
        /// Gets or sets the identifier of the service where the error originated.
        /// Defaults to "requestia-api".
        /// </summary>
        public string Service { get; set; } = "requestia-api";

        /// <summary>
        /// Gets or sets the date and time when the error occurred.
        /// The date is formatted as "dd/MM/yyyy hh:mm:ss".
        /// </summary>
        public string ErrorDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
    }
}
