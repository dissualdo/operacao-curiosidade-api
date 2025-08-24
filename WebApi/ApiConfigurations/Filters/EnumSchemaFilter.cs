using System.Text;
using System.Reflection; 
using System.ComponentModel;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.ApiConfigurations.Filters
{
    /// <summary>
    /// A schema filter that enhances Swagger documentation by appending descriptions for enum members.
    /// The descriptions are extracted from the <see cref="DescriptionAttribute"/> applied to each enum member.
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the filter to modify the OpenAPI schema for enum types.
        /// </summary>
        /// <param name="schema">The OpenAPI schema that is being generated.</param>
        /// <param name="context">The schema filter context containing type information.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Check if the current type is an enum.
            if (context.Type.IsEnum)
            {
                var enumDescriptions = new StringBuilder();

                // Loop through all enum member names.
                foreach (var enumName in Enum.GetNames(context.Type))
                {
                    // Retrieve the member information for the enum value.
                    var memberInfo = context.Type.GetMember(enumName).FirstOrDefault();
                    if (memberInfo != null)
                    {
                        // Get the DescriptionAttribute, if it exists.
                        var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                        var description = descriptionAttribute != null ? descriptionAttribute.Description : string.Empty;

                        // If a description is found, format it as "MemberName: Description".
                        // Otherwise, just add the member name.
                        if (!string.IsNullOrEmpty(description))
                            enumDescriptions.Append($"<li>{enumName}: {description}</li>");
                        else
                            enumDescriptions.Append($"<li>{enumName}</li>");
                    }
                }

                // Combine all the individual descriptions into one string with line breaks.
                schema.Description = string.Format("<ul>{0}</ul>", enumDescriptions);
            }
        }
    }
}
