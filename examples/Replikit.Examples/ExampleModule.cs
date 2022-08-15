using Kantaiko.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Examples.AdapterServices;
using Replikit.Examples.Messages;
using Replikit.Examples.State;
using Replikit.Examples.Users;
using Replikit.Examples.Views;

namespace Replikit.Examples;

public class ExampleModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<MessagesExampleModule>();
        services.AddModule<AdapterServicesExampleModule>();

        services.AddModule<StateExampleModule>();
        services.AddModule<ViewsExampleModule>();
        services.AddModule<UsersExampleModule>();
    }
}
