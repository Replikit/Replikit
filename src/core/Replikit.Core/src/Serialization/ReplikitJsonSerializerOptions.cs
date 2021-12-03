using System.Text.Json.Serialization;
using Replikit.Core.Serialization.Converters;

namespace Replikit.Core.Serialization;

public static class ReplikitJsonSerializerOptions
{
    public static IReadOnlyList<JsonConverter> Converters { get; } = new JsonConverter[]
    {
        new IdentifierConverter(),
        new GlobalIdentifierConverter()
    };
}
