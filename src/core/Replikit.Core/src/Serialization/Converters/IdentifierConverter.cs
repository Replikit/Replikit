using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.Serialization.Converters;

public class IdentifierConverter : JsonConverter<Identifier>
{
    public override Identifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.Number => reader.GetInt64(),
            JsonTokenType.String => reader.GetString()!,
            _ => throw new JsonException("Invalid identifier type. Expected string, number or null")
        };
    }

    public override void Write(Utf8JsonWriter writer, Identifier value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}
