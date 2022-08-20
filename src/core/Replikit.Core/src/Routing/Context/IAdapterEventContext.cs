using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Routing.Context;

public interface IAdapterEventContext<out TEvent> where TEvent : IBotEvent
{
    TEvent Event { get; }
    IServiceProvider ServiceProvider { get; }
    CancellationToken CancellationToken { get; }

    IAdapter Adapter { get; }

    IMessageCollection MessageCollection { get; }

    bool IsHandled { get; set; }
}
