using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Routing.Context;

public interface IBotEventContext
{
    IBotEvent Event { get; }

    IServiceProvider ServiceProvider { get; }
    CancellationToken CancellationToken { get; }

    IAdapter Adapter { get; }

    IMessageCollection MessageCollection { get; }

    bool IsHandled { get; set; }
}

public interface IBotEventContext<out TEvent> : IBotEventContext where TEvent : IBotEvent
{
    new TEvent Event { get; }

    IBotEvent IBotEventContext.Event => Event;
}
