using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using WebApi.ApiConfigurations.Filters;
using WebApi.ApiConfigurations.Middleware;

namespace WebApi.ApiConfigurations.Extensions
{ /// <summary>
  /// Provides extension methods for configuring the application's service pipeline and middleware.
  /// </summary>
    [ExcludeFromCodeCoverage]
    public static class PipelineExtension
    {
        /// <summary>
        /// Configures services for the API pipeline including custom filters and options.
        /// This method adds custom filters such as model state and exception filters to the controllers.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public static void ConfigurePipeline(this IServiceCollection services)
        {
            // Suppress the default model state invalid filter so custom filters can be used.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Add custom filters as scoped dependencies.
            services.AddScoped<ModelStateFilter>();

            // Configure controllers to use custom filters.
            services.AddControllers(config =>
            {
                // Custom model state filter for handling validation errors.
                config.Filters.Add<ModelStateFilter>();

                // Custom exception filter for handling exceptions.
                config.Filters.Add(typeof(ExceptionFilter));

                // Filter to validate model state before executing actions.
                config.Filters.Add(typeof(ValidateModelStateFilter));
            });

            // Uncomment the following line if you need to convert enums to strings in JSON.
            // .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            // Enable options pattern support.
            services.AddOptions();
        }

        /// <summary>
        /// Configures middleware components for the application.
        /// This method adds custom middleware such as the global exception middleware to the pipeline.
        /// </summary>
        /// <param name="app">The application builder used to configure the middleware pipeline.</param>
        public static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            // Use the global exception middleware to catch and format exceptions.
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
