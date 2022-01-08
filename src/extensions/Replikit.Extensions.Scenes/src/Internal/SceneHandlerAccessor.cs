using Kantaiko.Controllers;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection;
using Kantaiko.Controllers.Introspection.Factory;
using Kantaiko.Controllers.Result;
using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing;
using Replikit.Extensions.Scenes.ExecutionHandlers;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneHandlerAccessor : ISceneIntrospectionInfoAccessor
{
    public IntrospectionInfo IntrospectionInfo { get; }
    public IHandler<SceneContext, Task<ControllerExecutionResult>> Handler { get; }

    public SceneHandlerAccessor(HostInfo hostInfo, IServiceProvider serviceProvider)
    {
        var lookupTypes = hostInfo.Assemblies.SelectMany(x => x.GetTypes());

        var introspectionBuilder = new IntrospectionBuilder<SceneContext>();

        introspectionBuilder.SetServiceProvider(serviceProvider);
        introspectionBuilder.AddDefaultTransformation();
        introspectionBuilder.AddEndpointMatching();

        IntrospectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        var pipelineBuilder = new PipelineBuilder<SceneContext>();

        pipelineBuilder.AddEndpointMatching();
        pipelineBuilder.AddSubHandlerExecution();
        pipelineBuilder.AddControllerInstantiation(ServiceHandlerFactory.Instance);
        pipelineBuilder.AddHandler(new LoadSceneStateHandler());
        pipelineBuilder.AddHandler(new LoadSceneEndpointParametersHandler());
        pipelineBuilder.AddEndpointInvocation();
        pipelineBuilder.AddHandler(new CompleteTransitionAndSaveStateHandler());

        var handlers = pipelineBuilder.Build();

        Handler = ControllerHandlerFactory.CreateControllerHandler(IntrospectionInfo, handlers);
    }
}
