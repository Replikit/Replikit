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
    public IControllerHandler<SceneContext> Handler { get; }

    public SceneHandlerAccessor(HostInfo hostInfo, IServiceProvider serviceProvider)
    {
        var lookupTypes = hostInfo.Assemblies.SelectMany(x => x.GetTypes());

        var introspectionBuilder = new IntrospectionBuilder<SceneContext>();

        introspectionBuilder.SetServiceProvider(serviceProvider);
        introspectionBuilder.AddDefaultTransformation();
        introspectionBuilder.AddEndpointMatching();

        IntrospectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        var handlers = new HandlerCollection<SceneContext>();

        handlers.AddEndpointMatching();
        handlers.AddSubHandlerExecution();
        handlers.AddControllerInstantiation(ServiceHandlerFactory.Instance);
        handlers.Add(new LoadStateHandler());
        handlers.Add(new LoadSceneEndpointParametersHandler());
        handlers.AddEndpointInvocation();
        handlers.Add(new CompleteTransitionAndSaveStateHandler());

        Handler = ControllerHandlerFactory.CreateControllerHandler(IntrospectionInfo, handlers);
    }
}
