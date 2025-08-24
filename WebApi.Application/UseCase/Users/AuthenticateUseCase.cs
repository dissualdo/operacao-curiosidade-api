using Newtonsoft.Json;
using WebApi.Models.Dto;
using WebApi.Models.Enums;
using WebApi.Models.Shared;
using System.Security.Claims; 
using System.Security.Principal;
using WebApi.Models.Models.Users;
using WebApi.Models.Repositories; 
using WebApi.Models.Dto.Authenticate;
using WebApi.Models.Shared.Extension;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt; 
using WebApi.Application.UseCase.Users.Handler;


namespace WebApi.Application.UseCase.Users
{
    /// <summary>
    /// Handles user authentication use case.
    /// </summary>
    public class AuthenticateUseCase : IAuthenticateUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly WebApi.Models.Shared.JWT.JwtConfiguration _jwtConfiguration;

        /// <summary>
        /// Initializes the class with dependencies for logging, user repository, and JWT configuration.
        /// </summary>
        /// <param name="jwtConfiguration">Configuration settings for JWT tokens.</param>
        /// <param name="userRepository">Repository for accessing user data.</param>
        public AuthenticateUseCase(WebApi.Models.Shared.JWT.JwtConfiguration jwtConfiguration, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtConfiguration = jwtConfiguration;
        }

        /// <summary>
        /// Authenticates the user based on login credentials and generates a JWT token.
        /// </summary>
        /// <param name="user">Object containing login and password information.</param>
        /// <returns>A response containing authentication results and user details.</returns>
        /// <exception cref="AppException">Throws exception for invalid login or failed validation.</exception>
        public async Task<AuthenticateResponse> ExecuteAsync(AuthenticateDto user)
        {
            try
            {
                // Validates login credentials.
                if (!LoginValidate.IsValid(user))
                    throw new AppException(AppException.LoginNotRegistered);

                // Encrypts password and creates a filter for database query.
                var encryptedPassword = user?.Password?.ToHashString();
                var filter = new GetUserFilterDto() { Login = user?.Login, Password = encryptedPassword };

                // Retrieves user based on login and encrypted password.
                var userFounded = await _userRepository.GetByLoginAsync(filter);

                // If no user is found, raises an authentication exception.
                if (userFounded is null)
                    throw new AppException(AppException.LoginNotRegistered);
                 
                var createdDate = DateTime.UtcNow;

                // Generates claims and token for user.
                var claims = GenerateClaims(userFounded);
                var expirationDate = createdDate.AddHours(_jwtConfiguration.ExpirationTimeHours);
                var token = GenerateAccessToken(createdDate, expirationDate, userFounded.Name, claims);

                var response = new AuthenticateResponse(userFounded)
                { 
                    AccessToken = token,
                    Authenticated = true, 
                    ProfileType = userFounded?.Authentication?.Profile,
                    Created = createdDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss")
                };

                return response;
            }
            catch (Exception err)
            {
                throw;
            }
        }

        /// <summary>
        /// Generates claims for the authenticated user.
        /// </summary>
        /// <param name="user">The user object containing authentication information.</param>
        /// <returns>An array of claims.</returns>
        private Claim[] GenerateClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", user.Email),
                new Claim("username", user.Name),
                new Claim("user_id", user.Id.ToString()),
                new Claim("userhash", user.Id.ToString()),
                new Claim("profile", user.Authentication.Profile.ToString()),
                new Claim(ClaimTypes.Role, user.Authentication.Profile.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")), 

            };

            return claims.ToArray();
        }

        /// <summary>
        /// Generates a JWT access token using user claims and expiration details.
        /// </summary>
        private string GenerateAccessToken(DateTime createdDate, DateTime expirationDate, string genericIdentifier, Claim[] claims)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(genericIdentifier), claims);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience,
                SigningCredentials = _jwtConfiguration.SigninCredentials,
                Subject = identity,
                NotBefore = createdDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }

    }
}