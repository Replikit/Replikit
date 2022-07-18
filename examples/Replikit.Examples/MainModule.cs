using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Adapters.Telegram;
using Replikit.Core.Modularity;
using Replikit.Examples.AdapterServices;
using Replikit.Examples.Messages;
using Replikit.Examples.Scenes;
using Replikit.Examples.State;
using Replikit.Examples.Users;
using Replikit.Examples.Views;

namespace Replikit.Examples;

public class MainModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<MessagesExampleModule>();
        services.AddModule<AdapterServicesExample>();

        services.AddModule<ScenesExampleModule>();
        services.AddModule<ViewsExampleModule>();
        services.AddModule<StateExampleModule>();
        services.AddModule<UsersExampleModule>();
    }

    protected override void ConfigureAdapters(IAdapterLoaderOptions options)
    {
        options.AddTelegram();
    }
}
