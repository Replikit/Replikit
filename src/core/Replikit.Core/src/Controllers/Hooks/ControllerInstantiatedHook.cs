using Kantaiko.Hosting.Hooks;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.Hooks;

public class ControllerInstantiatedHook : IAsyncHook
{
    public ControllerInstantiatedHook(IEventContext<MessageReceivedEvent> context, Type controllerType)
    {
        Context = context;
        ControllerType = controllerType;
    }

    public IEventContext<MessageReceivedEvent> Context { get; }
    public Type ControllerType { get; }
}
