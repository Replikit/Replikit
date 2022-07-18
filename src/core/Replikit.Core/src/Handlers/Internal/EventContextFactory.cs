using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Handlers.Internal;

internal class EventContextFactory
{
    private readonly Dictionary<Type, Type> _eventContextTypes = new();

    public IAdapterEventContext<IAdapterEvent> CreateContext(IAdapterEvent @event, IAdapter adapter,
        IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        if (!_eventContextTypes.TryGetValue(@event.GetType(), out var contextType))
        {
            var baseContextType = @event switch
            {
                IChannelEvent => typeof(ChannelEventContext<>),
                _ => typeof(AdapterEventContext<>)
            };

            contextType = baseContextType.MakeGenericType(@event.GetType());
            _eventContextTypes[@event.GetType()] = contextType;
        }

        return (IAdapterEventContext<IAdapterEvent>) Activator.CreateInstance(contextType,
            @event, adapter, serviceProvider, cancellationToken)!;
    }
}
