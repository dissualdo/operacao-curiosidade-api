using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApi.ApiConfigurations.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AuthenticationExtension
    {
        /// <summary>
        /// Configura a autenticação JWT baseada nas configurações do appsettings.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];
            var signinKey = configuration["JwtSettings:SigninKey"];

            if (string.IsNullOrWhiteSpace(signinKey) || signinKey.Length < 32)
                throw new ArgumentException("JwtSettings:SigninKey precisa ter no mínimo 32 caracteres (256 bits).");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidateIssuer = true,
                    IssuerSigningKey = key,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true  
                };
            });
        }
    }
}
