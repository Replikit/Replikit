using System.Diagnostics;
using System.Globalization;
using Kantaiko.Properties.Immutable;
using Kantaiko.Routing.Events;
using Kantaiko.Routing.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers.Lifecycle;

namespace Replikit.Core.Handlers.Internal;

internal class AdapterEventHandler : IAdapterEventHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AdapterEventHandler> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IAdapterEventRouter _adapterEventRouter;
    private readonly IHandlerLifecycle _handlerLifecycle;

    public AdapterEventHandler(IServiceProvider serviceProvider, ILogger<AdapterEventHandler> logger,
        IHostApplicationLifetime applicationLifetime, IAdapterEventRouter adapterEventRouter,
        IHandlerLifecycle handlerLifecycle)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _applicationLifetime = applicationLifetime;
        _adapterEventRouter = adapterEventRouter;
        _handlerLifecycle = handlerLifecycle;
    }

    public async Task HandleAsync(IEvent @event, IAdapter adapter, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        var eventType = @event.GetType();
        _logger.LogDebug("Handling event of type {EventType}", eventType.Name);

        using var scope = _serviceProvider.CreateScope();

        var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken,
            _applicationLifetime.ApplicationStopping);

        var properties = ImmutablePropertyCollection.Empty.Set(new AdapterEventProperties(adapter));

        var context = (IEventContext<Event>) Activator.CreateInstance(
            typeof(EventContext<>).MakeGenericType(eventType),
            @event, scope.ServiceProvider, properties, cancellationTokenSource.Token)!;

        UpdateCultureInfo(@event);

        try
        {
            await _adapterEventRouter.Handler.Handle(context);

            using var lifecycleEventScope = scope.ServiceProvider.CreateScope();

            var eventContext = new EventContext<EventHandledEvent>(new EventHandledEvent(context),
                lifecycleEventScope.ServiceProvider, cancellationToken: cancellationTokenSource.Token);

            await _handlerLifecycle.EventHandled.Handle(eventContext);
        }
        catch (ChainEndedException)
        {
            _logger.LogDebug("Event was ignored since no appreciate handler was found");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred while handling an event");
            throw;
        }
        finally
        {
            _logger.LogDebug("Event handled in {Elapsed} ms", stopwatch.Elapsed.TotalMilliseconds);
            stopwatch.Stop();
        }
    }

    private void UpdateCultureInfo(IEvent @event)
    {
        Thread.CurrentThread.CurrentUICulture = @event is IAccountEvent accountEvent
            ? accountEvent.Account.CultureInfo ?? CultureInfo.InvariantCulture
            : CultureInfo.InvariantCulture;

        _logger.LogTrace("Updated CultureInfo of thread {ThreadId} to {CultureInfo}",
            Environment.CurrentManagedThreadId, Thread.CurrentThread.CurrentUICulture);
    }
}
