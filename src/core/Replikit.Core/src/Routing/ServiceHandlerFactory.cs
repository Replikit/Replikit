using Kantaiko.Controllers.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Routing;

public class ServiceHandlerFactory : IHandlerFactory
{
    public static ServiceHandlerFactory Instance { get; } = new();

    public object CreateHandler(Type handlerType, IServiceProvider serviceProvider)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, handlerType);
    }
}
