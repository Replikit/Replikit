namespace Replikit.Core.Routing.Internal;

internal class RoutingOptions
{
    public List<Action<IApplicationBuilder>> ConfigureDelegates { get; } = new();
    public List<Action<IApplicationBuilder>> PostConfigureDelegates { get; } = new();
}
