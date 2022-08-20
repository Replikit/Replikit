using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Core.Abstractions.State;

public record StateKey(
    StateKind Kind,
    BotIdentifier? BotId = null,
    Identifier? ChannelId = null,
    Identifier? AccountId = null,
    Identifier? MessagePartId = null,
    [property: JsonIgnore] Type? Type = null
)
{
    public string? TypeName
    {
        get => Type?.AssemblyQualifiedName!;
        init => Type ??= value is null ? null : Type.GetType(value);
    }

    public static StateKey FromChannelId(StateKind kind, GlobalIdentifier channelId)
    {
        return new StateKey(kind, channelId.BotId, channelId.Value);
    }

    public static StateKey FromAccountId(StateKind kind, GlobalIdentifier accountId)
    {
        return new StateKey(kind, accountId.BotId, AccountId: accountId.Value);
    }

    public static StateKey FromMessageId(StateKind kind, GlobalMessageIdentifier messageId)
    {
        return new StateKey(kind,
            messageId.ChannelId.BotId,
            messageId.ChannelId.Value,
            MessagePartId: messageId.Value.PartIdentifiers[0]
        );
    }
}
