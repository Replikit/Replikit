using Kantaiko.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Replikit.Core.Controllers;

public class ServiceInstanceFactory : IInstanceFactory
{
    private ServiceInstanceFactory() { }

    public object CreateInstance(Type type, IServiceProvider serviceProvider)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, type);
    }

    public static ServiceInstanceFactory Instance { get; } = new();
}
