using Replikit.Abstractions.Channels.Events;
using Replikit.Core.Routing;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Sessions.Internal;

internal class ChannelSessionMiddleware : IBotEventMiddleware
{
    private readonly ISessionManager _sessionManager;

    public ChannelSessionMiddleware(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public async Task HandleAsync(IBotEventContext context, BotEventDelegate next)
    {
        if (context.Event is not IChannelEvent channelEvent)
        {
            await next(context);
            return;
        }

        await using var sessionLock = await _sessionManager.AcquireSessionAsync(
            SessionKey.ForChannel(channelEvent.Channel.Id),
            context.CancellationToken
        );

        context.ChannelSession = sessionLock.Session;
        await next(context);
    }
}
