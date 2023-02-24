using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.Serialization.Json.Converters;

internal class GlobalIdentifierConverter : JsonConverter<GlobalIdentifier>
{
    private readonly IdentifierPersistenceMode _persistenceMode;

    public GlobalIdentifierConverter(IdentifierPersistenceMode persistenceMode)
    {
        _persistenceMode = persistenceMode;
    }

    public override GlobalIdentifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return GlobalIdentifier.TryParse(reader.GetString(), out var result) ? result : default;
    }

    public override void Write(Utf8JsonWriter writer, GlobalIdentifier value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_persistenceMode));
    }
}
