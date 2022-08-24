using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing;

public delegate Task BotEventMiddlewareDelegate(IBotEventContext context, BotEventDelegate next);
