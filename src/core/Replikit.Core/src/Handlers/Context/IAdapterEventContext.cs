using Kantaiko.Routing.Events;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers.Context;

public interface IAdapterEventContext<out TEvent> : IAsyncEventContext<TEvent> where TEvent : IAdapterEvent
{
    IAdapter Adapter { get; }
}
