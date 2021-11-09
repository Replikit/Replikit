using System.Diagnostics;
using System.Globalization;
using Kantaiko.Hosting.Hooks;
using Kantaiko.Routing;
using Kantaiko.Routing.Exceptions;
using Kantaiko.Routing.Handlers;
using Microsoft.Collections.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers.Hooks;

namespace Replikit.Core.Handlers.Internal;

internal class AdapterEventHandler : IAdapterEventHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AdapterEventHandler> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;

    public MultiValueDictionary<Type, Type> HandlerTypes { get; } = new();

    public AdapterEventHandler(IServiceProvider serviceProvider, ILogger<AdapterEventHandler> logger,
        IHostApplicationLifetime applicationLifetime)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _applicationLifetime = applicationLifetime;
    }

    public async Task HandleAsync(IEvent @event, IAdapter adapter, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        var eventType = @event.GetType();
        _logger.LogDebug("Handling event of type {EventType}", eventType.Name);

        using var scope = _serviceProvider.CreateScope();
        var hookDispatcher = scope.ServiceProvider.GetRequiredService<IHookDispatcher>();

        var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken,
            _applicationLifetime.ApplicationStopping);

        var context = (IEventContext) Activator.CreateInstance(typeof(EventContext<>).MakeGenericType(eventType),
            @event, adapter, scope.ServiceProvider, cancellationTokenSource.Token)!;

        try
        {
            UpdateCultureInfo(@event);

            var eventHandlingHook = new EventHandlingHook(context);
            await hookDispatcher.DispatchAsync(eventHandlingHook, cancellationToken);

            var eventContextAccessor = scope.ServiceProvider.GetRequiredService<EventContextAccessor>();
            eventContextAccessor.SetContext(context);

            var transientChainHandler = new TransientChainHandler<IEventContext, Task<Unit>>(
                HandlerTypes[eventType],
                DependencyInjectionHandlerFactory.Instance);

            await transientChainHandler.Handle(context);

            var eventHandledHook = new EventHandledHook(context);
            await hookDispatcher.DispatchAsync(eventHandledHook, cancellationToken);
        }
        catch (ChainEndedException)
        {
            _logger.LogDebug("Event was ignored since no appreciate handler was found");

            var eventHandledHook = new EventHandledHook(context);
            await hookDispatcher.DispatchAsync(eventHandledHook, cancellationToken);
        }
        catch (Exception exception)
        {
            var eventHandledHook = new EventHandledHook(context, exception);
            await hookDispatcher.DispatchAsync(eventHandledHook, cancellationToken);

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
