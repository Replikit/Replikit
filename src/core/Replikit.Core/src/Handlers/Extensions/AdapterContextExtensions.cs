using Kantaiko.Routing.Events;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers.Extensions;

public static class AdapterContextExtensions
{
    public static IAdapter? GetAdapter<TEvent>(this IEventContext<TEvent> context) where TEvent : IEvent
    {
        ArgumentNullException.ThrowIfNull(context);

        return AdapterContextProperties.Of(context)?.Adapter;
    }

    public static IAdapter GetRequiredAdapter<TEvent>(this IEventContext<TEvent> context) where TEvent : IEvent
    {
        return context.GetAdapter() ?? throw new InvalidOperationException("Failed to access an adapter instance");
    }
}
