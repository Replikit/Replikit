using Replikit.Core.Routing.Middleware;

namespace Replikit.Core.Routing;

public static class ApplicationBuilderExtensions
{
    public static void Use(this IApplicationBuilder app, AdapterEventMiddlewareDelegate middlewareDelegate)
    {
        ArgumentNullException.ThrowIfNull(app);
        ArgumentNullException.ThrowIfNull(middlewareDelegate);

        app.Use(next => context => middlewareDelegate(context, next));
    }

    public static void UseMiddleware<TMiddleware>(this IApplicationBuilder app)
        where TMiddleware : IAdapterEventMiddleware
    {
        ArgumentNullException.ThrowIfNull(app);

        var middleware = ServiceHandlerFactory.Instance.CreateHandler<TMiddleware>(app.ApplicationServices);

        app.Use(next => context => middleware.HandleAsync(context, next));
    }

    public static void UseMiddleware(this IApplicationBuilder app, IAdapterEventMiddleware middleware)
    {
        ArgumentNullException.ThrowIfNull(app);
        ArgumentNullException.ThrowIfNull(middleware);

        app.Use(next => context => middleware.HandleAsync(context, next));
    }
}
