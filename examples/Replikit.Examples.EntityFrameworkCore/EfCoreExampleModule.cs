using Kantaiko.Hosting.Modularity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Integrations.EntityFrameworkCore;

namespace Replikit.Examples.EntityFrameworkCore;

public class EfCoreExampleModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public EfCoreExampleModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<MainModule>();

        services.AddDbContext<ApplicationDbContext>(builder =>
        {
            var connectionString = _configuration.GetConnectionString("Default")!;

            builder.UseNpgsql(connectionString);
        });

        services.AddEntityFrameworkCoreModule()
            .AddDbContext<ApplicationDbContext>()
            .AddDefaults();
    }
}
