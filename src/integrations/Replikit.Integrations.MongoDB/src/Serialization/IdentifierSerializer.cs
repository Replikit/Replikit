using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class IdentifierSerializer : SerializerBase<Identifier>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Identifier value)
    {
        BsonSerializer.Serialize(context.Writer, value.Value);
    }

    public override Identifier Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return context.Reader.CurrentBsonType switch
        {
            BsonType.Int32 => context.Reader.ReadInt32(),
            BsonType.Int64 => context.Reader.ReadInt64(),
            BsonType.String => context.Reader.ReadString(),
            _ => throw new InvalidOperationException("Invalid Identifier type")
        };
    }
}
