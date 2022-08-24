using System.Globalization;
using Replikit.Abstractions.Accounts.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing.Middleware;

internal class ThreadLocalizationMiddleware : IBotEventMiddleware
{
    public Task HandleAsync(IBotEventContext context, BotEventDelegate next)
    {
        Thread.CurrentThread.CurrentUICulture = context.Event is IAccountEvent accountEvent
            ? accountEvent.Account.CultureInfo ?? CultureInfo.InvariantCulture
            : CultureInfo.InvariantCulture;

        return next(context);
    }
}
