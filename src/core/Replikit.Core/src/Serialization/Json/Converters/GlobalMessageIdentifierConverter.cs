using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Core.Serialization.Json.Converters;

internal class GlobalMessageIdentifierSerializer : JsonConverter<GlobalMessageIdentifier>
{
    public override GlobalMessageIdentifier Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        return GlobalMessageIdentifier.TryParse(reader.GetString(), out var result) ? result : default;
    }

    public override void Write(Utf8JsonWriter writer, GlobalMessageIdentifier value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
