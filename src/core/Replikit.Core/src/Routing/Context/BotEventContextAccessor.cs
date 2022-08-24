namespace Replikit.Core.Routing.Context;

internal class BotEventContextAccessor : IBotEventContextAccessor
{
    public IBotEventContext? CurrentContext { get; set; }
}
