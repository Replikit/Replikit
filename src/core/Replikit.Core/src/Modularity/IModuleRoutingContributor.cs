using Kantaiko.Modularity;
using Replikit.Core.Routing;

namespace Replikit.Core.Modularity;

public interface IModuleRoutingContributor
{
    void Configure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata);

    void PostConfigure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata);
}
