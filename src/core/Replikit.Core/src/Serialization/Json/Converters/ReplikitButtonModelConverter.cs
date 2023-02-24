using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Core.Buttons;
using Replikit.Core.Serialization.Dynamic;

namespace Replikit.Core.Serialization.Json.Converters;

internal class ReplikitButtonModelConverter : JsonConverter<ReplikitButtonModel>
{
    public override ReplikitButtonModel? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        // Format: [actionId, payload?]

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array");
        }

        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected action id");
        }

        var actionId = reader.GetString()!;

        if (!reader.Read())
        {
            throw new JsonException("Failed to read payload or end of array");
        }

        if (reader.TokenType == JsonTokenType.EndArray)
        {
            return new ReplikitButtonModel(actionId, null);
        }

        var payload = JsonSerializer.Deserialize<DynamicValue>(ref reader, options)!;

        if (!reader.Read() || reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("Expected end of array");
        }

        return new ReplikitButtonModel(actionId, payload);
    }

    public override void Write(Utf8JsonWriter writer, ReplikitButtonModel value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteStringValue(value.ActionId);

        if (value.Payload is { } payload)
        {
            JsonSerializer.Serialize(writer, payload, options);
        }

        writer.WriteEndArray();
    }
}
