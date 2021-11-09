namespace Replikit.Core.Handlers.Internal;

internal class EventContextAccessor : IEventContextAccessor
{
    public IEventContext? Context { get; private set; }

    public void SetContext(IEventContext context)
    {
        Context = context;
    }
}
