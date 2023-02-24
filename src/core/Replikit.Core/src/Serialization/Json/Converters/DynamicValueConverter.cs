using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Core.Serialization.Dynamic;

namespace Replikit.Core.Serialization.Json.Converters;

internal class DynamicValueConverter : JsonConverter<DynamicValue>
{
    public override DynamicValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);

        // TODO find a way not to create a new function on each call to capture the options
        return DynamicValue.FromFactory(element, (e, type) => ((JsonElement)e!).Deserialize(type, options));
    }

    public override void Write(Utf8JsonWriter writer, DynamicValue value, JsonSerializerOptions options)
    {
        var underlyingValue = value.GetUnderlyingValue();

        JsonSerializer.Serialize(writer, underlyingValue, options);
    }
}