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

        context.Writer.WriteName(nameof(GlobalIdentifier.AdapterId));
        BsonSerializer.Serialize(context.Writer, value.AdapterId);

        context.Writer.WriteName(nameof(GlobalIdentifier.Value));
        BsonSerializer.Serialize(context.Writer, value.Value);

        context.Writer.WriteEndDocument();
    }

    public override GlobalIdentifier Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        context.Reader.ReadStartDocument();

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(GlobalIdentifier.AdapterId))
        {
            throw new BsonSerializationException($"Expected field '{nameof(GlobalIdentifier.Value)}'");
        }

        var adapterId = BsonSerializer.Deserialize<AdapterIdentifier>(context.Reader);

        if (adapterId is null)
        {
            throw new BsonSerializationException("Adapter id must not be null");
        }

        if (context.Reader.ReadName(Utf8NameDecoder.Instance) is not nameof(GlobalIdentifier.Value))
        {
            throw new BsonSerializationException($"Expected field '{nameof(GlobalIdentifier.Value)}'");
        }

        var identifier = BsonSerializer.Deserialize<Identifier>(context.Reader);

        context.Reader.ReadEndDocument();

        return new GlobalIdentifier(adapterId, identifier);
    }
}
