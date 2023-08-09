using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Authenticator.Api.Swagger;

/// <summary>
/// Describe how swagger need work with enum
/// </summary>
[ExcludeFromCodeCoverage]
public class EnumSchemaFilter : ISchemaFilter
{
   /// <inheritdoc/>
   public void Apply(OpenApiSchema schema, SchemaFilterContext context)
   {
      if (!context.Type.IsEnum) return;
      PopulateSchema(ref schema, ref context);
   }

   private static void PopulateSchema(ref OpenApiSchema schema, ref SchemaFilterContext context)
   {
      var values = schema.Enum.ToArray();
      schema.Enum.Clear();
      for (var index = 0; index < Enum.GetNames(context.Type).ToList().Count; index++)
      {
         var n = Enum.GetNames(context.Type).ToList()[index];
         schema?.Enum?.Add(new OpenApiString($"{n} = {((OpenApiPrimitive<int>)values[index]).Value}"));
      }
   }
}