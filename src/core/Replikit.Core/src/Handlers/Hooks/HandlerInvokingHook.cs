using Kantaiko.Hosting.Hooks;

namespace Replikit.Core.Handlers.Hooks;

public class HandlerInvokingHook : IAsyncHook
{
    public HandlerInvokingHook(IEventContext context, Type handlerType)
    {
        Context = context;
        HandlerType = handlerType;
    }

    public IEventContext Context { get; }
    public Type HandlerType { get; }
}
