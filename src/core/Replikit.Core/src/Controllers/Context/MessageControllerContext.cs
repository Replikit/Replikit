using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Controllers.Context;

internal class MessageControllerContext : ChannelEventContext<MessageReceivedEvent>, IMessageControllerContext
{
    public MessageControllerContext(
        MessageReceivedEvent @event,
        IAdapter adapter,
        IServiceProvider? serviceProvider = null,
        CancellationToken cancellationToken = default
    ) : base(@event, adapter, serviceProvider, cancellationToken) { }
}
