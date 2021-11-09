using Kantaiko.Hosting.Hooks;
using Replikit.Core.Handlers.Hooks;
using Replikit.Extensions.Sessions.Internal;

namespace Replikit.Extensions.Sessions.HookHandlers.HandlerInvoking;

internal class LoadSessionsHandler : IAsyncHookHandler<HandlerInvokingHook>
{
    private readonly SessionManager _sessionManager;

    public LoadSessionsHandler(SessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public async Task HandleAsync(HandlerInvokingHook payload, CancellationToken cancellationToken)
    {
        await _sessionManager.Load(cancellationToken);
    }
}
