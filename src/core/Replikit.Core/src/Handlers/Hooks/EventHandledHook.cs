using Kantaiko.Hosting.Hooks;

namespace Replikit.Core.Handlers.Hooks;

public class EventHandledHook : IAsyncHook
{
    public EventHandledHook(IEventContext context, Exception? exception = null)
    {
        Context = context;
        Exception = exception;
    }

    public IEventContext Context { get; }
    public Exception? Exception { get; }
}
