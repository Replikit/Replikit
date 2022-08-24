namespace Replikit.Core.Routing.Context;

public interface IBotEventContextAccessor
{
    IBotEventContext? CurrentContext { get; }
}
