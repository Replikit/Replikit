using Kantaiko.Controllers;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Controllers.ParameterConversion;
using Kantaiko.Controllers.ParameterConversion.Text;
using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Configuration;
using Replikit.Core.Controllers.Configuration.Context;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Internal;

internal class ControllerHandlerAccessor : IControllerIntrospectionInfoAccessor, IMessageControllerConfiguration
{
    public IntrospectionInfo IntrospectionInfo { get; }
    public IControllerHandler<IMessageControllerContext> Handler { get; }

    public event SyncEventHandler<IControllerConfigurationContext<IMessageControllerContext>>? Configuring;

    public ControllerHandlerAccessor(HostInfo hostInfo, IServiceProvider serviceProvider)
    {
        var lookupTypes = hostInfo.Assemblies.SelectMany(x => x.GetTypes()).ToArray();

        var converterCollection = new TextParameterConverterCollection(lookupTypes);

        var introspectionBuilder = new IntrospectionBuilder<IMessageControllerContext>();

        introspectionBuilder.SetServiceProvider(serviceProvider);
        introspectionBuilder.AddDefaultTransformation();
        introspectionBuilder.AddEndpointMatching();
        introspectionBuilder.AddTextParameterConversion(converterCollection);

        var handlers = new HandlerCollection<IMessageControllerContext>();

        handlers.AddEndpointMatching();
        handlers.AddSubHandlerExecution();
        handlers.AddParameterConversion(h => h.AddTextParameterConversion(ServiceHandlerFactory.Instance));
        handlers.AddControllerInstantiation(ServiceHandlerFactory.Instance);
        handlers.AddEndpointInvocation();
        handlers.AddRequestCompletion();

        if (Configuring is not null)
        {
            using var scope = serviceProvider.CreateScope();

            var configurationContext = new ControllerConfigurationContext<IMessageControllerContext>(
                introspectionBuilder, handlers, scope.ServiceProvider);

            Configuring(configurationContext);
        }

        IntrospectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        Handler = ControllerHandlerFactory.CreateControllerHandler(IntrospectionInfo, handlers);
    }
}
