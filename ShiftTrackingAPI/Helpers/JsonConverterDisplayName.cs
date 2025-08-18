using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace ShiftTrackingAPI.Helpers
{
    /// <summary>
    /// кастомный метод вывода строковых значений из enum
    /// </summary>
    public class JsonConverterDisplayName: ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(name => schema.Enum.Add(new OpenApiString(name)));

                schema.Type = "string";
                schema.Format = null;
            }
        }
    }
}
