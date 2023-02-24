using System.Text.Json;
using JetBrains.Annotations;

namespace Replikit.Core.Serialization.Json;

[UsedImplicitly]
internal class ReplikitJsonSerializerOptions
{
    public JsonSerializerOptions SerializerOptions { get; } = new();
}
