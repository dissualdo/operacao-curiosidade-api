using WebApi.Models.Shared;

namespace WebApi.Models.Dto
{
    /// <summary>
    /// Data transfer object (DTO) for filtering user retrieval.
    /// </summary>
    public class GetUserFilterDto : FilterRequest
    {
        public long? Id { get; set; }

        /// <summary>
        /// User's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's login identifier (e.g., email or username).
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// User's full name or display name.
        /// </summary>
        public string UserName { get; set; } = string.Empty;


        /// <summary>
        /// Indicates whether the user is active or inactive.
        /// </summary>
        public bool ? IsActive { get; set; } = true;

        /// <summary>
        /// User's password for authentication purposes.
        /// Note: Ensure proper security measures when handling passwords.
        /// </summary>
        public string Password { get; set; } = string.Empty;
         
    }
}
