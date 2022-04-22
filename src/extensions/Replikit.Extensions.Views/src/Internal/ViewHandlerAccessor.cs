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
    public IHandler<ViewContext, Task<ControllerExecutionResult>> Handler { get; }

    public ViewHandlerAccessor(HostInfo hostInfo, IServiceProvider serviceProvider)
    {
        var lookupTypes = hostInfo.Assemblies.SelectMany(x => x.GetTypes());

        var introspectionBuilder = new IntrospectionBuilder<ViewContext>();

        introspectionBuilder.SetServiceProvider(serviceProvider);
        introspectionBuilder.AddDefaultTransformation();
        introspectionBuilder.AddEndpointMatching();

        IntrospectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        var pipelineBuilder = new PipelineBuilder<ViewContext>();

        pipelineBuilder.AddEndpointMatching();
        pipelineBuilder.AddSubHandlerExecution();
        pipelineBuilder.AddControllerInstantiation(ServiceHandlerFactory.Instance);
        pipelineBuilder.AddHandler(new LoadStateHandler());
        pipelineBuilder.AddHandler(new LoadViewEndpointParametersHandler());
        pipelineBuilder.AddEndpointInvocation();
        pipelineBuilder.AddHandler(ActivatorUtilities.CreateInstance<UpdateViewAndSaveStateHandler>(serviceProvider));

        var handlers = pipelineBuilder.Build();

        Handler = ControllerHandlerFactory.CreateControllerHandler(IntrospectionInfo, handlers);
    }
}
