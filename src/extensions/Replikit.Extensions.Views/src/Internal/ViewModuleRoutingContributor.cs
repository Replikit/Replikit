using Kantaiko.Modularity;
using Replikit.Core.Modularity;
using Replikit.Core.Routing;

namespace Replikit.Extensions.Views.Internal;

internal class ViewModuleRoutingContributor : IModuleRoutingContributor
{
    public void Configure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata)
    {
        app.UseViews(moduleType.Assembly);
    }

    public void PostConfigure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata) { }
}
