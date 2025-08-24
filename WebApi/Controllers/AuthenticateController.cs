using WebApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Shared.Security;
using WebApi.Models.Dto.Authenticate;
using WebApi.Application.UseCase.Users;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller responsible for managing user authentication endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticateUseCase _authenticateUseCase;

        /// <summary>
        /// Constructor to initialize dependencies for authentication use case and logging.
        /// </summary>
        /// <param name="authenticateUseCase">Use case handling user authentication process.</param>
        /// <param name="logger">Logger instance to log relevant information.</param>
        public AuthenticationController(IAuthenticateUseCase authenticateUseCase, ILogger<AuthenticationController> logger)
        {
            _logger = logger;
            _authenticateUseCase = authenticateUseCase;
        }

        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /api/Authentication
        /// {
        ///     "login": "user@example.com",
        ///     "password": "securePassword123"
        /// }
        /// 
        /// </remarks>
        /// <param name="login">Object containing the login and password for authentication.</param>
        /// <returns>An <see cref="AuthenticateResponse"/> object with authentication details, such as the token.</returns>
        /// <response code="200">Returns the authentication response containing user and token information.</response>
        /// <response code="400">If the login data is invalid or missing required fields.</response>
        /// <response code="500">If an internal server error occurs during the authentication process.</response>
        [HttpPost]
        [ProducesResponseType(typeof(AuthenticateResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<AuthenticateResponse> Post([FromBody] AuthenticateDto login)
        {
            try
            {
                // Calls the authentication use case and returns the result.
                var result = await _authenticateUseCase.ExecuteAsync(login);
                return result;
            }
            catch (System.Exception e)
            {
                // Logs exception details for debugging and diagnostics.
                _logger.LogInformation($"WebApi.Controllers.AuthenticationController: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a secure resource that requires user authentication.
        /// </summary>
        /// <remarks>
        /// This endpoint validates the JWT token and extracts the user's unique identifier (userhash claim).
        /// </remarks>
        /// <returns>Returns the authenticated user's ID and a flag indicating if the token is valid.</returns>
        [Authorize]
        [HttpGet("Validate")]
        public IActionResult GetAuthenticatedUser()
        {
            var userId = User.FindFirst("userhash")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User not authenticated." });
            }

            return Ok(new
            {
                IsValid = true,
                UserId = userId,
                Email = User.GetEmail(),
                Origin = User.GetOrigin(),
                Profile = User.GetProfile(),
                FullName = User.GetFullName(),
            });
        }  
    }
}
