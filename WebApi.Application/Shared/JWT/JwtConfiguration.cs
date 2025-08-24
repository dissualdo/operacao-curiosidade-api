using System.Text;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace WebApi.Application.Shared.JWT
{
    /// <summary>
    /// Provides configuration settings for JWT token generation.
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>
        /// Gets the intended audience for the JWT token.
        /// </summary>
        public string Audience { get; }

        /// <summary>
        /// Gets the issuer of the JWT token.
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// Gets the security key used to sign the token.
        /// </summary>
        [JsonIgnore]
        public SecurityKey Key { get; }

        /// <summary>
        /// Gets the token expiration time in hours.
        /// </summary>
        public int ExpirationTimeHours { get; }

        /// <summary>
        /// Gets the signing credentials based on the security key and algorithm.
        /// </summary>
        [JsonIgnore]
        public SigningCredentials SigninCredentials { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtConfiguration"/> class using configuration settings.
        /// </summary>
        /// <param name="configuration">The configuration instance to read JWT settings from.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when a required JWT setting is missing or invalid.
        /// </exception>
        public JwtConfiguration(IConfiguration configuration)
        {
            // Retrieve the signing key from the configuration
            var signinKey = configuration["JwtSettings:SigninKey"];
            if (string.IsNullOrWhiteSpace(signinKey))
                throw new ArgumentException("SigninKey is missing in configuration.");

            // Ensure the signing key has at least 256 bits (32 bytes)
            if (Encoding.UTF8.GetByteCount(signinKey) < 32)
                throw new ArgumentException("SigninKey must be at least 256 bits (32 bytes) long.");

            // Create a symmetric security key using the signing key
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));

            // Create signing credentials using the security key and HMAC-SHA256 algorithm
            SigninCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Retrieve and validate Audience setting
            Audience = configuration["JwtSettings:Audience"] ??
                       throw new ArgumentException("Audience is missing in configuration.");

            // Retrieve and validate Issuer setting
            Issuer = configuration["JwtSettings:Issuer"] ??
                     throw new ArgumentException("Issuer is missing in configuration.");

            // Retrieve and parse the expiration time setting
            if (!int.TryParse(configuration["JwtSettings:ExpirationTimeHours"], out int expiration))
                throw new ArgumentException("ExpirationTimeHours is invalid or missing in configuration.");

            ExpirationTimeHours = expiration;
        }
    }
}
