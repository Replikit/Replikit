using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Integrations.MongoDB.Serialization;

internal class GlobalMessageIdentifierSerializer : SerializerBase<GlobalMessageIdentifier>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        GlobalMessageIdentifier value)
    {
        context.Writer.WriteStartDocument();

        context.Writer.WriteName(nameof(GlobalMessageIdentifier.ChannelId));
        BsonSerializer.Serialize(context.Writer, value.ChannelId);

        context.Writer.WriteName(nameof(GlobalMessageIdentifier.Identifier));
        BsonSerializer.Serialize(context.Writer, value.Identifier);

        context.Writer.WriteEndDocument();
    }

    public override GlobalMessageIdentifier Deserialize(BsonDeserializationContext context,
        BsonDeserializationArgs args)
    {
        context.Reader.ReadStartDocument();

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(GlobalMessageIdentifier.ChannelId))
        {
            throw new BsonSerializationException($"Expected field '{nameof(GlobalMessageIdentifier.ChannelId)}'");
        }

        var channelId = BsonSerializer.Deserialize<GlobalIdentifier>(context.Reader);

        if (channelId == default)
        {
            throw new BsonSerializationException("Channel id must not be null");
        }

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(GlobalMessageIdentifier.Identifier))
        {
            throw new BsonSerializationException($"Expected field '{nameof(GlobalMessageIdentifier.Identifier)}'");
        }

        var identifier = BsonSerializer.Deserialize<MessageIdentifier>(context.Reader);

        context.Reader.ReadEndDocument();

        return new GlobalMessageIdentifier(channelId, identifier);
    }
}
