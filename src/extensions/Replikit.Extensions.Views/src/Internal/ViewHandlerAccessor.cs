using Kantaiko.Controllers;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Controllers.Result;
using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.Views.ExecutionHandlers;

namespace Replikit.Extensions.Views.Internal;

internal class ViewHandlerAccessor : IViewIntrospectionInfoAccessor
{
    public IntrospectionInfo IntrospectionInfo { get; }
    public IControllerHandler<ViewContext> Handler { get; }

    public ViewHandlerAccessor(HostInfo hostInfo, IServiceProvider serviceProvider)
    {
        var lookupTypes = hostInfo.Assemblies.SelectMany(x => x.GetTypes());

        var introspectionBuilder = new IntrospectionBuilder<ViewContext>();

        introspectionBuilder.SetServiceProvider(serviceProvider);
        introspectionBuilder.AddDefaultTransformation();
        introspectionBuilder.AddEndpointMatching();

        IntrospectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        var handlers = new HandlerCollection<ViewContext>();

        handlers.AddEndpointMatching();
        handlers.AddSubHandlerExecution();
        handlers.AddControllerInstantiation(ServiceHandlerFactory.Instance);
        handlers.Add(new LoadStateHandler());
        handlers.Add(new LoadViewEndpointParametersHandler());
        handlers.AddEndpointInvocation();
        handlers.Add(ActivatorUtilities.CreateInstance<UpdateViewAndSaveStateHandler>(serviceProvider));

        Handler = ControllerHandlerFactory.CreateControllerHandler(IntrospectionInfo, handlers);
    }
}
