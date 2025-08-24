using WebApi.Infra.Data;
using WebApi.Infra.Repositories;
using WebApi.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using WebApi.Application.UseCase.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.NativeInjector
{
    /// <summary>
    /// Provides dependency injection configuration for the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class NativeDependencyInjector
    {
        /// <summary>
        /// Registers application services, repositories, and database context.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="configuration">The application configuration containing connection strings.</param>
        public static void DependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Retrieve the database connection string from configuration 
            var connectionString = configuration.GetConnectionString("Api");

            // JWT configuration
            var jwtConfiguration = new WebApi.Models.Shared.JWT.JwtConfiguration(configuration);
            services.AddSingleton(jwtConfiguration);

            // Configure EF Core with MySQL (Pomelo)
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );


            #region .: Use Cases :.  
            services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
            services.AddScoped<IManagerUserUseCase, ManagerUserUseCase>();
            services.AddScoped<IAuthenticateUseCase, AuthenticateUseCase>(); 
            services.AddScoped<IGetUserByFilterUseCase, GetUserByFilterUseCase>();
            #endregion

            #region .: Repositories :.  
            services.AddScoped<IUserRepository, UserRepository>(); 
            #endregion
        }
    }
}
