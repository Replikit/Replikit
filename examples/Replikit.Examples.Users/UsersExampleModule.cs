using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Users;

namespace Replikit.Examples.Users;

public class UsersExampleModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<UsersModule>();
    }
}
