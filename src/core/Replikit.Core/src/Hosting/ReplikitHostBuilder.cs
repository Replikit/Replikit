using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Host;

namespace Replikit.Core.Hosting;

public class ReplikitHostBuilder : ManagedHostBuilder
{
    public ReplikitHostBuilder(string[]? args = null) : base(args)
    {
        Modules.Add<ReplikitCoreModule>();
        this.ConfigureHooks();
    }
}
