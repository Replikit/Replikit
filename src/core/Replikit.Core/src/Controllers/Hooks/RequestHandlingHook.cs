using Kantaiko.Hosting.Hooks;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.Hooks;

public class RequestHandlingHook : IAsyncHook
{
    public RequestHandlingHook(IEventContext<MessageReceivedEvent> context)
    {
        Context = context;
    }

    public IEventContext<MessageReceivedEvent> Context { get; }

    public bool ShouldProcess { get; set; } = true;
}
