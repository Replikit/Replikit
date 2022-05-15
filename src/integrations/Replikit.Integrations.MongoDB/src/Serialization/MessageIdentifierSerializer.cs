using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class MessageIdentifierSerializer : SerializerBase<MessageIdentifier>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        MessageIdentifier value)
    {
        BsonSerializer.Serialize(context.Writer, value.Identifiers.ToArray());
    }

    public override MessageIdentifier Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var identifiers = BsonSerializer.Deserialize<IReadOnlyList<Identifier>>(context.Reader);

        return new MessageIdentifier(identifiers);
    }
}
