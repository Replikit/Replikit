using Kantaiko.Hosting.Hooks;
using Replikit.Core.Handlers.Hooks;
using Replikit.Extensions.Sessions.Internal;

namespace Replikit.Extensions.Sessions.HookHandlers.EventHandled;

internal class SaveSessionsHandler : IAsyncHookHandler<EventHandledHook>
{
    private readonly SessionManager _sessionManager;

    public SaveSessionsHandler(SessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public async Task HandleAsync(EventHandledHook payload, CancellationToken cancellationToken)
    {
        await _sessionManager.Save(cancellationToken);
    }
}
