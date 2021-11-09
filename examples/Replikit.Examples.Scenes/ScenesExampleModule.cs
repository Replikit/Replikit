using Replikit.Core.Modules;
using Replikit.Extensions.Scenes;

namespace Replikit.Examples.Scenes;

public class ScenesExampleModule : ReplikitModule
{
    public override void ConfigureModules(IReplikitModuleCollection modules)
    {
        modules.Add<ScenesModule>();
    }
}
