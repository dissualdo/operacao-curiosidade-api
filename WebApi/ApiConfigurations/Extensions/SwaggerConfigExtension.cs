using System.Reflection;
using Microsoft.OpenApi.Models;
using WebApi.ApiConfigurations.Filters;

namespace WebApi.ApiConfigurations.Extensions
{
    /// <summary>
    /// Extension class to configure Swagger for API documentation.
    /// </summary>
    public static class SwaggerConfigExtension
    {
        /// <summary>
        /// Configures Swagger using the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add Swagger services to.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Build the path to the XML documentation file.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Include the XML comments (if the file exists).
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApi.Models.xml"));
                    c.SchemaFilter<EnumSchemaFilter>();
                }
            });


            services.AddSwaggerGen(c =>
            {
                // Sets the XML comments path for Swagger documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Configures schema IDs for models to use fully qualified names
                c.CustomSchemaIds(x => x.FullName);

                // Groups API actions by area and controller
                c.TagActionsBy(x =>
                {
                    var r = x.ActionDescriptor.RouteValues;

                    return new[] { r["controller"] };
                });

                // Orders API actions by area, controller, and HTTP method
                c.OrderActionsBy(x =>
                {
                    var r = x.ActionDescriptor.RouteValues;
                    return r["controller"] + " - " + x.HttpMethod;
                });

                // Defines the security scheme for JWT-based authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                };

                // Adds security requirements to the API documentation
                var security = new OpenApiSecurityRequirement()
                {
                    {
                        securityScheme,
                        new List<string>()
                    }
                };

                // Adds the Swagger documentation with title and version
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Operação Curiosidade API V1", Version = "v1" });

                // Adds security definitions for JWT authentication
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description =
                            @"<h5>JWT Authorization header using the Bearer scheme.</h5><small>Example: Bearer 12345abcdef</small>",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                // Applies the security requirements to the Swagger setup
                c.AddSecurityRequirement(security);
            });
        }
    }
}