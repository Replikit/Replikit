using Kantaiko.Modularity;
using Replikit.Core.Modularity;
using Replikit.Core.Routing;

namespace Replikit.Core.Controllers;

internal class ControllerModuleRoutingContributor : IModuleRoutingContributor
{
    public void Configure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata)
    {
        app.UseControllers(moduleType.Assembly);
    }

    public void PostConfigure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata) { }
}
