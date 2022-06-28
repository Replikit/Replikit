using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Common;

public static class ServiceCollectionExtensions
{
    public static void AddEntityUsageIndicator<TEntity>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IEntityUsageIndicator<TEntity>, EntityUsageIndicator<TEntity>>();
    }
}
