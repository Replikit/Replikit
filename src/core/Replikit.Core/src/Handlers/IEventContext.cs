using Kantaiko.Routing.Context;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers;

public interface IEventContext : IHasCancellationToken, IHasServiceProvider
{
    IEvent Event { get; }
    IAdapter Adapter { get; }
}

public interface IEventContext<out TEvent> : IEventContext where TEvent : IEvent
{
    new TEvent Event { get; }
}
