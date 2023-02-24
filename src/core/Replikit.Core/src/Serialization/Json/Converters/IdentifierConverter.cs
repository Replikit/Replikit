using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.Serialization.Json.Converters;

internal class IdentifierConverter : JsonConverter<Identifier>
{
    public override Identifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null => default,
            JsonTokenType.Number => reader.GetInt64(),
            JsonTokenType.String when Guid.TryParse(reader.GetString()!, out var guid) => guid,
            JsonTokenType.String => reader.GetString()!,
            _ => throw new JsonException("Invalid identifier type. Expected string, number or null")
        };
    }

    public override void Write(Utf8JsonWriter writer, Identifier value, JsonSerializerOptions options)
    {
        if (value.GetUnderlyingValue() is { } underlyingValue)
        {
            JsonSerializer.Serialize(writer, underlyingValue, options);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
