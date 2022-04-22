using Kantaiko.Routing.Context;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Core.EntityCollections;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitEntityCollections(this IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            var context = sp.GetRequiredService<IContext>();

            return context is IEventContext<IChannelEvent> channelEventContext
                ? channelEventContext.GetRequiredMessageCollection()
                : throw new ReplikitException("Cannot access message collection outside a channel event scope");
        });
    }
}
