using System.Text.Json;
using System.Text.Json.Serialization;

namespace Replikit.Integrations.EntityFrameworkCore.Serialization;

internal class TypeConverter : JsonConverter<Type>
{
    public override Type? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString() is { } typeName ? Type.GetType(typeName) : null;
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.AssemblyQualifiedName!);
    }
}
