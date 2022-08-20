using System.Globalization;
using Replikit.Abstractions.Accounts.Events;
using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Routing.Middleware;

internal class ThreadLocalizationMiddleware : IAdapterEventMiddleware
{
    public Task HandleAsync(IAdapterEventContext<IBotEvent> context, AdapterEventDelegate next)
    {
        Thread.CurrentThread.CurrentUICulture = context.Event is IAccountEvent accountEvent
            ? accountEvent.Account.CultureInfo ?? CultureInfo.InvariantCulture
            : CultureInfo.InvariantCulture;

        return next(context);
    }
}
