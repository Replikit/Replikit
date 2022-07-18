using Kantaiko.Hosting.Modularity.TypeRegistration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Internal;
using Replikit.Core.Handlers.Lifecycle;
using Replikit.Core.Handlers.TypeRegistration;

namespace Replikit.Core.Handlers;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<Internal.AdapterEventHandler>();

        services.AddTypeRegistrationHandler<AdapterEventHandlerRegistrationHandler>();

        services.AddSingleton<EventContextFactory>();
        services.AddSingleton<AdapterEventRouter>();

        services.TryAddSingleton<IAdapterEventHandler>(provider =>
            provider.GetRequiredService<Internal.AdapterEventHandler>());

        services.AddSingleton<HandlerLifecycle>();
        services.AddSingleton<IHandlerLifecycle>(provider => provider.GetRequiredService<HandlerLifecycle>());
    }
}
