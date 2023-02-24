using System.Text.Json;
using Microsoft.Extensions.Options;
using Replikit.Core.Persistence;

namespace Replikit.Core.Serialization.Json;

internal class ReplikitJsonSerializer : IReplikitJsonSerializer
{
    private readonly JsonSerializerOptions _serializerOptions;

    public ReplikitJsonSerializer(IOptions<ReplikitJsonSerializerOptions> serializerOptions,
        IOptions<ReplikitPersistenceOptions> persistenceOptions)
    {
        _serializerOptions = serializerOptions.Value.SerializerOptions;
        _serializerOptions.AddReplikitConverters(persistenceOptions.Value.PersistenceMode);
    }

    public string Serialize(object? value)
    {
        return JsonSerializer.Serialize(value, _serializerOptions);
    }

    public T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _serializerOptions);
    }
}
