using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Adapters.Telegram.Tests.Shared.Serialization;

internal class TestAttachmentConverter : JsonConverter<Attachment>
{
    public override Attachment Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Attachment value, JsonSerializerOptions options)
    {
        // It's not possible to serialize Attachment directly,
        // because System.Text.Json serializes only base properties of Attachment.
        // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism

        JsonSerializer.Serialize<object>(writer, value, options);
    }
}
