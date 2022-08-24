using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;
using Replikit.Core.Routing.Internal;

namespace Replikit.Core.Routing;

public static class ServiceCollectionExtensions
{
    internal static void AddRoutingInternal(this IServiceCollection services)
    {
        services.AddSingleton<EventContextFactory>();

        services.AddSingleton<AdapterEventDispatcher>();
        services.AddSingleton<IAdapterEventDispatcher>(sp => sp.GetRequiredService<AdapterEventDispatcher>());

        services.AddScoped<BotEventContextAccessor>();
        services.AddScoped<IBotEventContextAccessor>(sp => sp.GetRequiredService<BotEventContextAccessor>());

        services.AddScoped<IBotEventContext>(sp =>
        {
            var eventContextAccessor = sp.GetRequiredService<IBotEventContextAccessor>();

            return eventContextAccessor.CurrentContext ??
                   throw new ReplikitException("No event context available inside the scope.");
        });
    }

    public static void ConfigureReplikitApplication(this IServiceCollection services,
        Action<IApplicationBuilder> configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureDelegate);

        services.Configure<RoutingOptions>(options => options.ConfigureDelegates.Add(configureDelegate));
    }

    public static void PostConfigureReplikitApplication(this IServiceCollection services,
        Action<IApplicationBuilder> configureDelegate)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureDelegate);

        services.Configure<RoutingOptions>(options => options.PostConfigureDelegates.Add(configureDelegate));
    }
}
