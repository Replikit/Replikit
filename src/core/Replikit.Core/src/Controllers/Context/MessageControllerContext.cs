using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.EntityCollections;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Controllers.Context;

internal class MessageControllerContext : IMessageControllerContext
{
    private readonly IAdapterEventContext<MessageReceivedEvent> _eventContext;

    public MessageControllerContext(IAdapterEventContext<MessageReceivedEvent> eventContext)
    {
        _eventContext = eventContext;
    }

    public MessageReceivedEvent Event => _eventContext.Event;
    public IServiceProvider ServiceProvider => _eventContext.ServiceProvider;
    public CancellationToken CancellationToken => _eventContext.CancellationToken;
    public IAdapter Adapter => _eventContext.Adapter;
    public IMessageCollection MessageCollection => _eventContext.MessageCollection;

    public bool IsHandled
    {
        get => _eventContext.IsHandled;
        set => _eventContext.IsHandled = value;
    }
}
