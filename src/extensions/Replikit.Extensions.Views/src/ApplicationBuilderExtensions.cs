using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Routing;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

public static class ApplicationBuilderExtensions
{
    private class ViewRoutingProperties
    {
        public bool MiddlewareRegistered { get; set; }
        public HashSet<Assembly> UsedAssemblies { get; } = new();
    }

    public static void UseViews<T>(this IApplicationBuilder app)
    {
        UseViews(app, typeof(T).Assembly);
    }

    public static void UseViews(this IApplicationBuilder app, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(app);

        var routingProperties = app.Properties.GetOrCreate<ViewRoutingProperties>();

        if (!routingProperties.MiddlewareRegistered)
        {
            routingProperties.MiddlewareRegistered = true;
            app.UseMiddleware<ViewMiddleware>();
        }

        if (!routingProperties.UsedAssemblies.Contains(assembly))
        {
            routingProperties.UsedAssemblies.Add(assembly);

            var handlerFactory = app.ApplicationServices.GetRequiredService<ViewHandlerFactory>();
            handlerFactory.InitializeHandlers(assembly);
        }
    }
}
