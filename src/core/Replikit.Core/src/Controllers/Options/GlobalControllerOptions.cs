using Replikit.Core.Controllers.Configuration;

namespace Replikit.Core.Controllers.Options;

internal class GlobalControllerOptions
{
    public List<Action<ControllerConfigurationBuilder>> ConfigureDelegates { get; } = new();
}
