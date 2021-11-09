using Kantaiko.Hosting.Hooks;

namespace Replikit.Core.Handlers.Hooks;

public class EventHandlingHook : IAsyncHook
{
    public EventHandlingHook(IEventContext context)
    {
        Context = context;
    }

    public IEventContext Context { get; }
}
