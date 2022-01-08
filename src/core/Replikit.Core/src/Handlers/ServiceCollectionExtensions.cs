using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Lifecycle;

namespace Replikit.Core.Handlers;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<Internal.AdapterEventHandler>();

        services.AddSingleton<IAdapterEventRouter, AdapterEventRouter>();

        services.TryAddSingleton<IAdapterEventHandler>(provider =>
            provider.GetRequiredService<Internal.AdapterEventHandler>());

        services.AddSingleton<IHandlerLifecycle, HandlerLifecycle>();
    }
}
