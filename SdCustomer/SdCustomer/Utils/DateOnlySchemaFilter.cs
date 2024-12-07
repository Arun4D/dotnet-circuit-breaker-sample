using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SdCustomer.Utils
{
    public class DateOnlySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(DateOnly))
            {
                schema.Type = "string";
                schema.Format = "date";  // Use ISO-8601 format (yyyy-MM-dd)
            }
        }
    }
}
