using Kantaiko.Routing.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Handlers.Internal;

public class DependencyInjectionHandlerFactory : IHandlerFactory
{
    public object CreateHandler(Type handlerType, IServiceProvider serviceProvider)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, handlerType);
    }

    private static DependencyInjectionHandlerFactory? _instance;
    public static DependencyInjectionHandlerFactory Instance => _instance ??= new DependencyInjectionHandlerFactory();
}
