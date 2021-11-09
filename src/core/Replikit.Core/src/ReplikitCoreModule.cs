using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers.Internal;
using Replikit.Core.Hosting;
using Replikit.Core.Localization;
using Replikit.Core.Logging;
using Replikit.Core.Modules;
using Replikit.Core.Services;

namespace Replikit.Core;

public class ReplikitCoreModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public ReplikitCoreModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICancellationTokenProvider, CancellationTokenProvider>();

        services.AddReplikitLogging();
        services.AddReplikitHandlers();
        services.AddReplikitControllers();
        services.AddReplikitLocalization();
        services.AddReplikitEntityCollections();
        services.AddReplikitAdapters(_configuration);
    }
}
