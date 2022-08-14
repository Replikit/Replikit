using System.Reflection;
using Replikit.Core.Controllers.Configuration;
using Replikit.Core.Routing;

namespace Replikit.Core.Controllers;

public static class ApplicationBuilderExtensions
{
    private class RoutingControllersProperties
    {
        public HashSet<Assembly> UsedAssemblies { get; } = new();
    }

    public static void UseControllers<T>(this IApplicationBuilder app)
    {
        UseControllers(app, typeof(T).Assembly);
    }

    public static void UseControllers(this IApplicationBuilder app, Assembly assembly,
        Action<ControllerConfigurationBuilder>? configureDelegate = null)
    {
        ArgumentNullException.ThrowIfNull(app);

        var routingProperties = app.Properties.GetOrCreate<RoutingControllersProperties>();

        if (routingProperties.UsedAssemblies.Contains(assembly))
        {
            return;
        }

        routingProperties.UsedAssemblies.Add(assembly);

        app.UseMiddleware(new ControllerMiddleware(assembly, app.ApplicationServices, configureDelegate));
    }
}
