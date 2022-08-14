using Kantaiko.Controllers.Handlers;

namespace Replikit.Core.Routing;

// TODO move to Kantaiko.Controllers
public static class HandlerFactoryExtensions
{
    public static THandler CreateHandler<THandler>(this IHandlerFactory handlerFactory,
        IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(handlerFactory);

        return (THandler) handlerFactory.CreateHandler(typeof(THandler), serviceProvider);
    }
}
