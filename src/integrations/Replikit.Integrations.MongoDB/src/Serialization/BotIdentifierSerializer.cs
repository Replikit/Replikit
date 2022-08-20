using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class BotIdentifierSerializer : SerializerBase<BotIdentifier>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, BotIdentifier value)
    {
        context.Writer.WriteStartDocument();

        context.Writer.WriteName(nameof(BotIdentifier.PlatformId));
        BsonSerializer.Serialize(context.Writer, value.PlatformId);

        context.Writer.WriteName(nameof(BotIdentifier.Value));
        BsonSerializer.Serialize(context.Writer, value.Value);

        context.Writer.WriteEndDocument();
    }

    public override BotIdentifier Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        context.Reader.ReadStartDocument();

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(BotIdentifier.PlatformId))
        {
            throw new BsonSerializationException($"Expected field '{nameof(BotIdentifier.PlatformId)}'");
        }

        var platformId = context.Reader.ReadString();

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(BotIdentifier.Value))
        {
            throw new BsonSerializationException($"Expected field '{nameof(BotIdentifier.Value)}'");
        }

        var identifier = BsonSerializer.Deserialize<Identifier>(context.Reader);

        context.Reader.ReadEndDocument();

        return new BotIdentifier(platformId, identifier);
    }
}
