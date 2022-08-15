using Kantaiko.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Extensions.Views;

namespace Replikit.Examples.AdapterServices;

public class AdapterServicesExampleModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<ReplikitViewsModule>();
    }
}
