using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Extensions.State;

namespace Replikit.Examples.State;

public class StateExampleModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<StateModule>();
    }
}
