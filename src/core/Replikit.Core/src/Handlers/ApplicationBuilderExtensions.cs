using System.Reflection;
using Replikit.Core.Routing;

namespace Replikit.Core.Handlers;

public static class ApplicationBuilderExtensions
{
    private class RoutingHandlerProperties
    {
        public HashSet<Assembly> UsedAssemblies { get; } = new();
    }

    public static void UseHandlers<T>(this IApplicationBuilder app)
    {
        UseHandlers(app, typeof(T).Assembly);
    }

    public static void UseHandlers(this IApplicationBuilder app, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(app);

        var routingProperties = app.Properties.GetOrCreate<RoutingHandlerProperties>();

        if (!routingProperties.UsedAssemblies.Contains(assembly))
        {
            routingProperties.UsedAssemblies.Add(assembly);

            app.UseMiddleware(new HandlerMiddleware(assembly));
        }
    }
}
