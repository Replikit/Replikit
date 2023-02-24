using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.Serialization.Json.Converters;

internal class BotIdentifierConverter : JsonConverter<BotIdentifier>
{
    public override BotIdentifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return BotIdentifier.TryParse(reader.GetString(), out var result) ? result : default;
    }

    public override void Write(Utf8JsonWriter writer, BotIdentifier value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
