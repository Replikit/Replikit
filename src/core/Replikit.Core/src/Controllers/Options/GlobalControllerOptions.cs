using Replikit.Core.Controllers.Configuration;

namespace Replikit.Core.Controllers.Options;

public class GlobalControllerOptions
{
    private readonly List<Action<ControllerConfigurationBuilder>> _configureDelegates = new();

    public IReadOnlyList<Action<ControllerConfigurationBuilder>> ConfigureDelegates => _configureDelegates;

    public void Configure(Action<ControllerConfigurationBuilder> configureDelegate)
    {
        _configureDelegates.Add(configureDelegate);
    }
}
