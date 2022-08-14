using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Core.Serialization.Converters;

internal class MessageIdentifierConverter : JsonConverter<MessageIdentifier>
{
    public override MessageIdentifier Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        var identifiers = JsonSerializer.Deserialize<IReadOnlyList<Identifier>>(ref reader, options);

        return identifiers is null ? default : new MessageIdentifier(identifiers);
    }

    public override void Write(Utf8JsonWriter writer, MessageIdentifier value, JsonSerializerOptions options)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (value.PartIdentifiers is not null)
        {
            JsonSerializer.Serialize(writer, value.PartIdentifiers, options);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
