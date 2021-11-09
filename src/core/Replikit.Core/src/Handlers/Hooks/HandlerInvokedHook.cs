using Kantaiko.Hosting.Hooks;

namespace Replikit.Core.Handlers.Hooks;

public class HandlerInvokedHook : IAsyncHook
{
    public HandlerInvokedHook(IEventContext context, Type handlerType, Exception? exception = null)
    {
        Context = context;
        HandlerType = handlerType;
        Exception = exception;
    }

    public IEventContext Context { get; }
    public Type HandlerType { get; }
    public Exception? Exception { get; }
}
