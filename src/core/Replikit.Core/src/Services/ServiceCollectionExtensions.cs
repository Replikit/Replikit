using Kantaiko.Routing.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Services;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitServices(this IServiceCollection services)
    {
        services.AddScoped<ContextAccessor>();
        services.AddScoped<IContextAccessor>(sp => sp.GetRequiredService<ContextAccessor>());
        services.AddScoped<IContextAcceptor>(sp => sp.GetRequiredService<ContextAccessor>());
        services.AddScoped(typeof(ContextAccessor<>));

        services.AddScoped<ICancellationTokenProvider, CancellationTokenProvider>();
    }
}
