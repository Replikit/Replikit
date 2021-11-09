using Replikit.Abstractions.Adapters.Loader;
using Replikit.Adapters.Telegram;
using Replikit.Core.Modules;
using Replikit.Examples.AdapterServices;
using Replikit.Examples.Messages;
using Replikit.Examples.Scenes;
using Replikit.Examples.Sessions;
using Replikit.Examples.Views;

namespace Replikit.Examples;

public class MainModule : ReplikitModule
{
    public override void ConfigureModules(IReplikitModuleCollection modules)
    {
        modules.Add<MessagesExampleModule>();
        modules.Add<AdapterServicesExample>();

        modules.Add<ScenesExampleModule>();
        modules.Add<ViewsExampleModule>();
        modules.Add<SessionsExampleModule>();
    }

    public override void ConfigureAdapters(IAdapterLoaderOptions options)
    {
        options.AddTelegram();
    }
}
