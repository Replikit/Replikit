using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Core.Common;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class ObjectDynamicValueSerializer : SerializerBase<object>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        DynamicValueSerializer.Instance.Serialize(context, args, (DynamicValue) value);
    }

    public override object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return DynamicValueSerializer.Instance.Deserialize(context, args);
    }
}
