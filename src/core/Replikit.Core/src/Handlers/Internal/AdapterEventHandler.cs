using System.Diagnostics;
using System.Globalization;
using Kantaiko.Routing.Exceptions;
using Kantaiko.Routing.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers.Context;
using Replikit.Core.Handlers.Lifecycle;

namespace Replikit.Core.Handlers.Internal;

internal class AdapterEventHandler : IAdapterEventHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AdapterEventHandler> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly HandlerLifecycle _handlerLifecycle;
    private readonly EventContextFactory _eventContextFactory;
    private readonly AdapterEventRouter _eventRouter;

    public AdapterEventHandler(IServiceProvider serviceProvider,
        ILogger<AdapterEventHandler> logger,
        IHostApplicationLifetime applicationLifetime,
        HandlerLifecycle handlerLifecycle,
        EventContextFactory eventContextFactory,
        AdapterEventRouter eventRouter)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _applicationLifetime = applicationLifetime;
        _handlerLifecycle = handlerLifecycle;
        _eventContextFactory = eventContextFactory;
        _eventRouter = eventRouter;
    }

    public async Task HandleAsync(IAdapterEvent @event, IAdapter adapter, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        var eventType = @event.GetType();
        _logger.LogDebug("Handling event of type {EventType}", eventType.Name);

        await using var scope = _serviceProvider.CreateAsyncScope();

        var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            _applicationLifetime.ApplicationStopping
        );

        var context = (IAdapterEventContext<AdapterEvent>) _eventContextFactory.CreateContext(@event,
            adapter, scope.ServiceProvider, cancellationTokenSource.Token);

        UpdateCultureInfo(@event);

        try
        {
            await _eventRouter.Handler.Handle(context);

            await _handlerLifecycle.OnEventHandledAsync(context, cancellationTokenSource.Token);
        }
        catch (NoHandlersLeftException)
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

    private void UpdateCultureInfo(IAdapterEvent @event)
    {
        Thread.CurrentThread.CurrentUICulture = @event is IAccountEvent accountEvent
            ? accountEvent.Account.CultureInfo ?? CultureInfo.InvariantCulture
            : CultureInfo.InvariantCulture;

        _logger.LogTrace("Updated CultureInfo of thread {ThreadId} to {CultureInfo}",
            Environment.CurrentManagedThreadId, Thread.CurrentThread.CurrentUICulture);
    }
}
