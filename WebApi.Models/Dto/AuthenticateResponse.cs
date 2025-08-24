using WebApi.Models.Enums;
using WebApi.Models.Models.Users;

namespace WebApi.Models.Dto
{
    /// <summary>
    /// DTO (Data Transfer Object) representing the response of an authentication process.
    /// </summary>
    /// <remarks>
    /// Initializes an instance of <see cref="AuthenticateResponse"/> using user data.
    /// </remarks>
    /// <param name="user">The authenticated user's details.</param>
    public class AuthenticateResponse(User user)
    {
        /// <summary>
        /// User's unique identifier.
        /// </summary>
        public long IdUser { get; set; } = user.Id;

        /// <summary>
        /// User's email address for contact or notifications.
        /// </summary>
        public string Email { get; set; } = user.Email;

        /// <summary>
        /// Expiration date of the authentication token.
        /// </summary>
        public string Expiration { get; set; } = string.Empty;

        /// <summary>
        /// The JWT (JSON Web Token) granted to the user for authentication.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the user is authenticated successfully.
        /// </summary>
        public bool Authenticated { get; set; }

        /// <summary>
        /// Profile type of the user (e.g., Admin, User).
        /// </summary>
        public EProfile ? ProfileType { get; set; }


        /// <summary>
        /// Date and time of authentication token creation in the format "yyyy-MM-dd HH:mm:ss".
        /// </summary>
        public string Created { get; set; } = string.Empty;

        /// <summary>
        /// Unique identifier for the user's profile.
        /// </summary>
        public long ProfileId { get; internal set; }
    }
}
