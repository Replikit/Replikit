using Kantaiko.Properties;

namespace Replikit.Core.Routing;

public interface IApplicationBuilder
{
    IServiceProvider ApplicationServices { get; }

    IPropertyCollection Properties { get; }

    void Use(Func<AdapterEventDelegate, AdapterEventDelegate> middleware);
}
