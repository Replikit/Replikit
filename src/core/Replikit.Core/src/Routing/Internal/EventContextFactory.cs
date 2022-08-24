using System.Collections.Concurrent;
using System.Linq.Expressions;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing.Internal;

internal class EventContextFactory
{
    private delegate IBotEventContext<IBotEvent> FactoryDelegate(IBotEvent @event, IAdapter adapter,
        IServiceProvider serviceProvider, CancellationToken cancellationToken);

    private readonly ConcurrentDictionary<Type, FactoryDelegate> _factoryDelegates = new();

    public IBotEventContext<IBotEvent> CreateContext(IBotEvent @event, IAdapter adapter,
        IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var factoryDelegate = _factoryDelegates.GetOrAdd(@event.GetType(), CreateFactoryDelegate);

        return factoryDelegate(@event, adapter, serviceProvider, cancellationToken);
    }

    private static FactoryDelegate CreateFactoryDelegate(Type eventType)
    {
        var @event = Expression.Parameter(typeof(IBotEvent));
        var castedEvent = Expression.Convert(@event, eventType);

        var adapter = Expression.Parameter(typeof(IAdapter));
        var serviceProvider = Expression.Parameter(typeof(IServiceProvider));
        var cancellationToken = Expression.Parameter(typeof(CancellationToken));

        var constructor = typeof(BotEventContext<>)
            .MakeGenericType(eventType)
            .GetConstructors()
            .Single();

        var instantiation = Expression.New(
            constructor,
            castedEvent,
            adapter,
            serviceProvider,
            cancellationToken
        );

        var returnExpression = Expression.Convert(
            instantiation,
            typeof(IBotEventContext<IBotEvent>)
        );

        return Expression.Lambda<FactoryDelegate>(
            returnExpression,
            @event,
            adapter,
            serviceProvider,
            cancellationToken
        ).Compile();
    }
}
