using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.EntityCollections;
using Replikit.Core.Routing.Context;

namespace Replikit.Extensions.Views.Actions;

internal class ViewActionContext : IViewActionContext
{
    private readonly IAdapterEventContext<ButtonPressedEvent> _eventContext;

    public ViewActionContext(IAdapterEventContext<ButtonPressedEvent> eventContext)
    {
        _eventContext = eventContext;
    }

    public ButtonPressedEvent Event => _eventContext.Event;

    public IServiceProvider ServiceProvider => _eventContext.ServiceProvider;

    public CancellationToken CancellationToken => _eventContext.CancellationToken;

    public IAdapter Adapter => _eventContext.Adapter;

    public IMessageCollection MessageCollection => _eventContext.MessageCollection;

    public bool IsHandled
    {
        get => _eventContext.IsHandled;
        set => _eventContext.IsHandled = value;
    }

    public void SuppressAutoUpdate()
    {
        UpdateRequested = false;
    }

    public void Update()
    {
        UpdateRequested = true;
    }

    internal bool UpdateRequested { get; private set; } = true;
}
