using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers;
using Replikit.Core.EntityCollections;
using Replikit.Core.GlobalServices;
using Replikit.Core.Handlers;
using Replikit.Core.Hosting;
using Replikit.Core.Localization;
using Replikit.Core.Logging;
using Replikit.Core.Modularity;
using Replikit.Core.Services;

namespace Replikit.Core;

[Module(Flags = ModuleFlags.Library)]
public class ReplikitCoreModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public ReplikitCoreModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddReplikitServices();
        services.AddApplicationLifecycle();
        services.AddReplikitLogging();
        services.AddReplikitHandlers();
        services.AddReplikitControllers();
        services.AddReplikitLocalization();
        services.AddReplikitEntityCollections();
        services.AddReplikitGlobalServices();
        services.AddReplikitHosting(_configuration);
    }
}
