using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Replikit.Core.Serialization.Json.Converters;

internal class CultureInfoConverter : JsonConverter<CultureInfo>
{
    public override CultureInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() is { } cultureName ? CultureInfo.GetCultureInfo(cultureName) : null;
    }

    public override void Write(Utf8JsonWriter writer, CultureInfo value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Name);
    }
}
