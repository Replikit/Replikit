using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing.Middleware;

public interface IBotEventMiddleware
{
    Task HandleAsync(IBotEventContext context, BotEventDelegate next);
}
