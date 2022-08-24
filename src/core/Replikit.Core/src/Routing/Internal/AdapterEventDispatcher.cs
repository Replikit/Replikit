using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing.Internal;

internal class AdapterEventDispatcher : IAdapterEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AdapterEventDispatcher> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly EventContextFactory _eventContextFactory;
    private readonly BotEventDelegate _eventDelegate;

    public AdapterEventDispatcher(IServiceProvider serviceProvider, ILogger<AdapterEventDispatcher> logger,
        IHostApplicationLifetime applicationLifetime, EventContextFactory eventContextFactory,
        IOptions<RoutingOptions> routingOptions)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _applicationLifetime = applicationLifetime;
        _eventContextFactory = eventContextFactory;

        var applicationBuilder = new ApplicationBuilder(_serviceProvider);

        foreach (var configureDelegate in routingOptions.Value.ConfigureDelegates)
        {
            configureDelegate(applicationBuilder);
        }

        foreach (var postConfigureDelegate in routingOptions.Value.PostConfigureDelegates)
        {
            postConfigureDelegate(applicationBuilder);
        }

        _eventDelegate = applicationBuilder.Build();
    }

    public async Task DispatchAsync(IBotEvent @event, IAdapter adapter,
        CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();

        var eventType = @event.GetType();
        _logger.LogDebug("Handling event of type {EventType}", eventType.Name);

        var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            _applicationLifetime.ApplicationStopping
        );

        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = _eventContextFactory.CreateContext(
            @event,
            adapter,
            scope.ServiceProvider,
            cancellationTokenSource.Token
        );

        var contextAccessor = scope.ServiceProvider.GetRequiredService<BotEventContextAccessor>();
        contextAccessor.CurrentContext = context;

        try
        {
            await _eventDelegate(context);
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
}
