namespace Replikit.Core.Handlers;

public interface IEventContextAccessor
{
    public IEventContext? Context { get; }
}
