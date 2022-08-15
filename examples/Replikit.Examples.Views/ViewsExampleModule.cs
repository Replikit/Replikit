using Kantaiko.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Extensions.Views;

namespace Replikit.Examples.Views;

public class ViewsExampleModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<ReplikitViewsModule>();
    }
}
