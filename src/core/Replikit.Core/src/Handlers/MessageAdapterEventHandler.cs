using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Core.Handlers;

public abstract class MessageAdapterEventHandler<TEvent> : ChannelAdapterEventHandler<TEvent>
    where TEvent : MessageEvent
{
    protected AccountInfo Account => Event.Account;
    protected Message Message => Event.Message;
}
