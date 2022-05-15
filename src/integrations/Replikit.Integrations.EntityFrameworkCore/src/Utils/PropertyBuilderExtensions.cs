using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Replikit.Integrations.EntityFrameworkCore.Utils;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(this PropertyBuilder<TProperty> builder,
        JsonSerializerOptions options)
    {
        return builder.HasConversion(
            x => JsonSerializer.Serialize(x, options),
            x => JsonSerializer.Deserialize<TProperty>(x, options)!
        );
    }
}
