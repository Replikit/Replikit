using System.Text.Json.Serialization;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Core.Abstractions.State;

public record StateKey(
    StateType StateType,
    AdapterIdentifier? AdapterId = null,
    Identifier? ChannelId = null,
    Identifier? AccountId = null,
    MessageIdentifier? MessageId = null,
    [property: JsonIgnore] Type? Type = null
)
{
    public string? TypeName
    {
        get => Type?.AssemblyQualifiedName!;
        init => Type ??= value is null ? null : Type.GetType(value);
    }

    public static StateKey FromChannelId(StateType type, GlobalIdentifier channelId)
    {
        return new StateKey(type, channelId.AdapterId, channelId);
    }

    public static StateKey FromAccountId(StateType type, GlobalIdentifier accountId)
    {
        return new StateKey(type, accountId.AdapterId, AccountId: accountId);
    }

    public static StateKey FromMessageId(StateType type, GlobalMessageIdentifier messageId)
    {
        return new StateKey(type,
            messageId.ChannelId.AdapterId,
            messageId.ChannelId,
            MessageId: messageId
        );
    }
}
