using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.State.Implementation;

public readonly record struct StateKey(
    StateType StateType,
    AdapterIdentifier? AdapterId = null,
    Identifier? ChannelId = null,
    Identifier? AccountId = null,
    MessageIdentifier? MessageId = null,
    Type? Type = null
)
{
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
