using Kantaiko.Modularity;
using Replikit.Core.Modularity;
using Replikit.Core.Routing;

namespace Replikit.Core.Handlers;

internal class HandlerModuleRoutingContributor : IModuleRoutingContributor
{
    public void Configure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata)
    {
        app.UseHandlers(moduleType.Assembly);
    }

    public void PostConfigure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata) { }
}
