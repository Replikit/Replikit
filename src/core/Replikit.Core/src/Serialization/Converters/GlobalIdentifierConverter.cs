using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.Serialization.Converters;

public class GlobalIdentifierConverter : JsonConverter<GlobalIdentifier>
{
    private readonly record struct GlobalIdentifierModel(AdapterIdentifier AdapterId, Identifier Value);

    public override GlobalIdentifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var (adapterId, identifier) = JsonSerializer.Deserialize<GlobalIdentifierModel>(ref reader, options);

        return new GlobalIdentifier(identifier, adapterId);
    }

    public override void Write(Utf8JsonWriter writer, GlobalIdentifier value, JsonSerializerOptions options)
    {
        var model = new GlobalIdentifierModel(value.AdapterId, value);

        JsonSerializer.Serialize(writer, model, options);
    }
}
