using System.Security.Claims;

namespace WebApi.Models.Shared.Security
{
    /// <summary>
    /// Provides helper methods to extract user claims from a ClaimsPrincipal.
    /// </summary>
    public static class JwtClaimHelper
    {
        /// <summary>
        /// Retrieves the user ID ("userhash") from the token claims.
        /// </summary>
        public static long? GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst("userhash")?.Value;
            return long.TryParse(value, out var id) ? id : null;
        }

        /// <summary>
        /// Retrieves the full name ("username") from the token claims.
        /// </summary>
        public static string? GetFullName(this ClaimsPrincipal user)
        {
            return user.FindFirst("username")?.Value;
        }

        /// <summary>
        /// Retrieves the user's email from the token claims.
        /// </summary>
        public static string? GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }

        /// <summary>
        /// Retrieves the user's profile (role) from the token claims.
        /// </summary>
        public static string? GetProfile(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }

        /// <summary>
        /// Retrieves the user's origin (e.g., country/system) from the token claims.
        /// </summary>
        public static string? GetOrigin(this ClaimsPrincipal user)
        {
            return user.FindFirst("origin")?.Value;
        }
    }
}
