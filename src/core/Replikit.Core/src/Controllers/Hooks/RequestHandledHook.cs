using Kantaiko.Controllers.Result;
using Kantaiko.Hosting.Hooks;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.Hooks;

public class RequestHandledHook : IAsyncHook
{
    public RequestHandledHook(IEventContext<MessageReceivedEvent> context, RequestProcessingResult result)
    {
        Context = context;
        Result = result;
    }

    public IEventContext<MessageReceivedEvent> Context { get; }
    public RequestProcessingResult Result { get; }

    public bool ShouldRespond { get; set; } = true;
}
