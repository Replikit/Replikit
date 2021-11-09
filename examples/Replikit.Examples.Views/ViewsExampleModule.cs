using Replikit.Core.Modules;
using Replikit.Extensions.Views;

namespace Replikit.Examples.Views;

public class ViewsExampleModule : ReplikitModule
{
    public override void ConfigureModules(IReplikitModuleCollection modules)
    {
        modules.Add<ViewsModule>();
    }
}
