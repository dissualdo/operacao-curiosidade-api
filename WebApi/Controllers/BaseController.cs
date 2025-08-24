using WebApi.Models.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    /// <summary>
    /// Base controller class that provides shared functionalities across all controllers.
    /// </summary> 
    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// Gets the user profile type of the logged-in user as an enumeration.
        /// </summary>
        /// <returns><see cref="EProfile"/> indicating the user's profile type.</returns>
        protected EProfile Profile => GetEnumProfile();

        /// <summary>
        /// Retrieves the username of the logged-in user.
        /// </summary>
        /// <returns>A string containing the username of the user.</returns>
        protected string UserName => GetClaim("username");

        /// <summary>
        /// Retrieves the profile of the logged-in user.
        /// </summary>
        /// <returns>A string containing the user's profile (e.g., "Admin", "User").</returns>
        protected string UserProfile => GetProfile();

        /// <summary>
        /// Retrieves the Authorization header from the request, containing the authentication token.
        /// </summary>
        protected string AuthHeader => HttpContext.Request.Headers["Authorization"];

        /// <summary>
        /// Retrieves the user ID of the logged-in user.
        /// </summary>
        /// <returns>A long value representing the user ID.</returns>
        protected long UserID => !string.IsNullOrEmpty(GetClaim("userhash")) ? long.Parse(GetClaim("userhash")) : 0;

        /// <summary>
        /// Converts the user's profile type to an <see cref="EProfile"/> enumeration value.
        /// </summary>
        /// <returns>The profile type as an <see cref="EProfile"/>.</returns>
        private EProfile GetEnumProfile()
        {
            var profile = GetProfile();
            return profile switch
            {
                "Admin" => EProfile.Admin,
                "User" => EProfile.User,
                "System" => EProfile.System,
                _ => EProfile.User,
            };
        }

        /// <summary>
        /// Retrieves a specific claim value for the logged-in user based on the provided key.
        /// </summary>
        /// <param name="key">The key identifying the claim to be retrieved.</param>
        /// <returns>A string containing the claim value.</returns>
        private string GetClaim(string key)
        {
            var user = HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var id = user.Claims.ToList().Find(t => t.Type.Equals(key));
                return id?.Value ?? string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Retrieves the profile of the logged-in user.
        /// </summary>
        /// <returns>A string indicating the user's profile type (e.g., "Admin").</returns>
        private string? GetProfile()
        {
            var user = HttpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var claim = user.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role);
                return claim?.Value ?? string.Empty;
            }

            return string.Empty;
        }

    
    }
}
