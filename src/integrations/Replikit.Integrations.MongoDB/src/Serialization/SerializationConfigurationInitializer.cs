using MongoDB.Bson.Serialization;
using Replikit.Core.Abstractions.State;

namespace Replikit.Integrations.MongoDB.Serialization;

internal static class SerializationConfigurationInitializer
{
    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized) return;

        BsonSerializer.RegisterSerializer(new IdentifierSerializer());
        BsonSerializer.RegisterSerializer(new GlobalIdentifierSerializer());
        BsonSerializer.RegisterSerializer(new MessageIdentifierSerializer());
        BsonSerializer.RegisterSerializer(new GlobalMessageIdentifierSerializer());
        BsonSerializer.RegisterSerializer(new BotIdentifierSerializer());
        BsonSerializer.RegisterSerializer(ObjectSerializer.Instance);

        BsonClassMap.RegisterClassMap<StateKey>(map =>
        {
            map.AutoMap();

            map.UnmapProperty(x => x.Type);
        });

        BsonClassMap.RegisterClassMap<StateItem>(map =>
        {
            map.MapIdProperty(x => x.Key);
            map.MapProperty(x => x.Value);
        });

        _initialized = true;
    }
}
