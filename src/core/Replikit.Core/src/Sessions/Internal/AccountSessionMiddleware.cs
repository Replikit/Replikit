using Replikit.Abstractions.Accounts.Events;
using Replikit.Core.Routing;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Sessions.Internal;

internal class AccountSessionMiddleware : IBotEventMiddleware
{
    private readonly ISessionManager _sessionManager;

    public AccountSessionMiddleware(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public async Task HandleAsync(IBotEventContext context, BotEventDelegate next)
    {
        if (context.Event is not IAccountEvent accountEvent)
        {
            await next(context);
            return;
        }

        await using var sessionLock = await _sessionManager.AcquireSessionAsync(
            SessionKey.ForAccount(accountEvent.Account.Id),
            context.CancellationToken
        );

        context.Session = sessionLock.Session;
        await next(context);
    }
}
