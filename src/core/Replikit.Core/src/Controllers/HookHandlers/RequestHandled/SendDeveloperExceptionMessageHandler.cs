using Kantaiko.Controllers.Result;
using Kantaiko.Hosting.Hooks;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Core.Controllers.Hooks;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Core.Controllers.HookHandlers.RequestHandled;

internal class SendDeveloperExceptionMessageHandler : IAsyncHookHandler<RequestHandledHook>
{
    public async Task HandleAsync(RequestHandledHook payload, CancellationToken cancellationToken)
    {
        if (!payload.ShouldRespond || payload.Result.ExitReason is not ExceptionExitReason exitReason) return;

        payload.ShouldRespond = false;

        var exceptionMessage = new MessageBuilder()
            .AddCodeLine("An unhandled exception occurred while processing the request.")
            .AddCodeLine(exitReason.Exception.ToString());

        await payload.Context.GetMessageCollection().SendAsync(exceptionMessage, cancellationToken: cancellationToken);
    }
}
