using Replikit.Core.Modules;
using Replikit.Extensions.Sessions;

namespace Replikit.Examples.Sessions;

public class SessionsExampleModule : ReplikitModule
{
    public override void ConfigureModules(IReplikitModuleCollection modules)
    {
        modules.Add<SessionsModule>();
    }
}
