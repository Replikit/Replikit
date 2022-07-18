using Kantaiko.Routing.Context;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.EntityCollections;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitEntityCollections(this IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            var context = sp.GetRequiredService<IContext>();

            return context is IChannelEventContext<IChannelEvent> channelEventContext
                ? channelEventContext.MessageCollection
                : throw new ReplikitException("Cannot access message collection outside a channel event scope");
        });
    }
}
