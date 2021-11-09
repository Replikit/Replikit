using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Messages.Events;

public abstract class MessageEvent : AccountAndChannelEvent
{
    public MessageEvent(AdapterIdentifier adapterId, Message message, ChannelInfo channelInfo,
        AccountInfo accountInfo) : base(adapterId, channelInfo, accountInfo)
    {
        Message = message;
    }

    public Message Message { get; }
}
