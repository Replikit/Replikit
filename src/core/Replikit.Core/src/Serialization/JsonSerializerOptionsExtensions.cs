using System.Text.Json;
using Replikit.Core.Serialization.Converters;

namespace Replikit.Core.Serialization;

public static class JsonSerializerOptionsExtensions
{
    public static void AddReplikitConverters(this JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.Converters.Add(new IdentifierConverter());
        options.Converters.Add(new MessageIdentifierConverter());
    }
}
