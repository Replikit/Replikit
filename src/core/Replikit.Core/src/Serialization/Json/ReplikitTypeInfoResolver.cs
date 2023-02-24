using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Replikit.Abstractions.Common.CustomData;

namespace Replikit.Core.Serialization.Json;

internal class ReplikitTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        if (!type.IsAssignableTo(typeof(IHasCustomData)))
        {
            return base.GetTypeInfo(type, options);
        }

        // We exclude CustomData property from serialization because it cannot be deserialized properly.
        var typeInfo = base.GetTypeInfo(type, options);
        var customDataProperty = typeInfo.Properties.First(x => x.Name == nameof(IHasCustomData.CustomData));
        typeInfo.Properties.Remove(customDataProperty);

        return typeInfo;
    }
}
