using Kantaiko.Modularity;
using Kantaiko.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers;
using Replikit.Core.EntityCollections;
using Replikit.Core.GlobalServices;
using Replikit.Core.Handlers;
using Replikit.Core.Localization;
using Replikit.Core.Modularity;
using Replikit.Core.Routing;

namespace Replikit.Core;

[Module(Flags = ModuleFlags.Library)]
public class ReplikitCoreModule : ReplikitModule
{
    protected override bool ConfigureDefaults => false;

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddRoutingInternal();
        services.AddHandlersInternal();
        services.AddControllersInternal();
        services.AddLocalizationInternal();
        services.AddEntityCollectionsInternal();
        services.AddGlobalServicesInternal();
    }
}
