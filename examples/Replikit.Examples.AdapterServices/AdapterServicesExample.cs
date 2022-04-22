using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Views;

namespace Replikit.Examples.AdapterServices;

public class AdapterServicesExample : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<ViewsModule>();
    }
}
