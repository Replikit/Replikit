using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.EntityCollections;

internal static class ServiceCollectionExtensions
{
    public static void AddEntityCollectionsInternal(this IServiceCollection services)
    {
        services.AddScoped<IMessageCollection>(sp =>
        {
            var contextAccessor = sp.GetRequiredService<IBotEventContextAccessor>();

            return MessageCollection.Create(contextAccessor.CurrentContext);
        });
    }
}
