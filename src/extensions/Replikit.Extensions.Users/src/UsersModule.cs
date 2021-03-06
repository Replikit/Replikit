using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Common;
using Replikit.Core.Modules;
using Replikit.Extensions.Users.Parameters;

namespace Replikit.Extensions.Users;

[Module(Flags = ModuleFlags.Library)]
public class UsersModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddEntityUsageIndicator<ReplikitUser>();

        services.AddScoped(typeof(UserManager<,>));
        services.AddScoped(typeof(ICurrentUserManager<,>), typeof(CurrentUserManager<,>));

        services.AddTransient(typeof(CurrentUserConverter<,>));
    }
}
