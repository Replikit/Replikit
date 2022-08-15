using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Core.Common;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class ObjectSerializer : SerializerBase<object>
{
    public static ObjectSerializer Instance { get; } = new();

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        BsonSerializer.Serialize(context.Writer, value.GetType(), value);
    }

    public override object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
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
