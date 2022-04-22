using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Extensions.Storage.Models;

namespace Replikit.Extensions.Storage.Serialization;

public class DynamicValueConverter : JsonConverter<DynamicValue>
{
    public override DynamicValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonObject = JsonSerializer.Deserialize<JsonElement>(ref reader, options);

        return new DynamicValue(type => jsonObject.Deserialize(type, options));
    }

    public override void Write(Utf8JsonWriter writer, DynamicValue value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}
