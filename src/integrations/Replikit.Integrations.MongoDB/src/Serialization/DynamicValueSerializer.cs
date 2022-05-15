using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Core.Common;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class DynamicValueSerializer : SerializerBase<DynamicValue>
{
    public static DynamicValueSerializer Instance { get; } = new();

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DynamicValue value)
    {
        BsonSerializer.Serialize(context.Writer, value.Value);
    }

    public override DynamicValue Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonObject = BsonSerializer.Deserialize<BsonValue>(context.Reader);

        return new DynamicValue(type =>
        {
            return bsonObject switch
            {
                BsonDocument bsonDocument => BsonSerializer.Deserialize(bsonDocument, type),
                _ => BsonTypeMapper.MapToDotNetValue(bsonObject)
            };
        });
    }
}
