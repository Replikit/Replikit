using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers.Internal;

public static class ServiceCollectionExtensions
{
    public static void AddReplikitHandlers(this IServiceCollection services)
    {
        services.TryAddSingleton<AdapterEventHandler>();
        services.TryAddSingleton<IAdapterEventHandler>(provider =>
            provider.GetRequiredService<AdapterEventHandler>());

        services.TryAddScoped<EventContextAccessor>();
        services.TryAddScoped<IEventContextAccessor>(
            provider => provider.GetRequiredService<EventContextAccessor>());
    }
}
