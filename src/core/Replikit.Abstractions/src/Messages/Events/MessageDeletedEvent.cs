using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Messages.Events;

public class MessageDeletedEvent : MessageEvent
{
    public MessageDeletedEvent(AdapterIdentifier adapterId, Message message, ChannelInfo channelInfo,
        AccountInfo accountInfo) : base(adapterId, message, channelInfo, accountInfo) { }
}
