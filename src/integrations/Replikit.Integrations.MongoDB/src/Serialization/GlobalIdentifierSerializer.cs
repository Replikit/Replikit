using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class GlobalIdentifierSerializer : SerializerBase<GlobalIdentifier>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, GlobalIdentifier value)
    {
        context.Writer.WriteStartDocument();

        context.Writer.WriteName(nameof(GlobalIdentifier.BotId));
        BsonSerializer.Serialize(context.Writer, value.BotId);

        context.Writer.WriteName(nameof(GlobalIdentifier.Value));
        BsonSerializer.Serialize(context.Writer, value.Value);

        context.Writer.WriteEndDocument();
    }

    public override GlobalIdentifier Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        context.Reader.ReadStartDocument();

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(GlobalIdentifier.BotId))
        {
            throw new BsonSerializationException($"Expected field '{nameof(GlobalIdentifier.Value)}'");
        }

        var botId = BsonSerializer.Deserialize<BotIdentifier>(context.Reader);

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(GlobalIdentifier.Value))
        {
            throw new BsonSerializationException($"Expected field '{nameof(GlobalIdentifier.Value)}'");
        }

        var identifier = BsonSerializer.Deserialize<Identifier>(context.Reader);

        context.Reader.ReadEndDocument();

        return new GlobalIdentifier(botId, identifier);
    }
}
