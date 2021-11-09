using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Handlers;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Core.EntityCollections;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitEntityCollections(this IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            var eventContextAccessor = sp.GetRequiredService<IEventContextAccessor>();

            return eventContextAccessor.Context is IEventContext<IChannelEvent> channelEventContext
                ? channelEventContext.GetMessageCollection()
                : throw new ReplikitException("Cannot access message collection outside a channel event scope");
        });
    }
}
