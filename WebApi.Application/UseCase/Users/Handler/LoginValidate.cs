using WebApi.Models.Dto.Authenticate;

namespace WebApi.Application.UseCase.Users.Handler
{
    /// <summary>
    /// Provides validation methods for user login credentials.
    /// </summary>
    public static class LoginValidate
    {
        /// <summary>
        /// Validates whether the provided authentication data is valid.
        /// </summary>
        /// <param name="user">The authentication data containing login and password.</param>
        /// <returns>True if both login and password are provided; otherwise, false.</returns>
        public static bool IsValid(AuthenticateDto user)
        {
            if (user == null)
                return false;

            return !string.IsNullOrWhiteSpace(user.Login) && !string.IsNullOrWhiteSpace(user.Password);
        }
    }
}
