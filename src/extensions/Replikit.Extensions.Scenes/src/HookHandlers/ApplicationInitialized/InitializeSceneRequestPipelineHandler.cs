using Kantaiko.Controllers;
using Kantaiko.Controllers.Middleware;
using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Hooks.ApplicationHooks;
using Replikit.Core.Controllers;
using Replikit.Extensions.Common.RequestHandler;
using Replikit.Extensions.Scenes.Internal;

namespace Replikit.Extensions.Scenes.HookHandlers.ApplicationInitialized;

internal class InitializeSceneRequestPipelineHandler : IHookHandler<ApplicationInitializedHook>
{
    private readonly SceneRequestHandlerAccessor _sceneRequestHandlerAccessor;
    private readonly IServiceProvider _serviceProvider;

    public InitializeSceneRequestPipelineHandler(SceneRequestHandlerAccessor sceneRequestHandlerAccessor,
        IServiceProvider serviceProvider)
    {
        _sceneRequestHandlerAccessor = sceneRequestHandlerAccessor;
        _serviceProvider = serviceProvider;
    }

    public void Handle(ApplicationInitializedHook payload)
    {
        var assemblies = payload.Assemblies.ToArray();

        var controllerCollection = ControllerCollection.FromAssemblies(assemblies);
        var middlewareCollection = MiddlewareCollection.FromAssemblies(assemblies);

        _sceneRequestHandlerAccessor.RequestHandler = new RequestHandler<SceneContext>(
            controllerCollection,
            new SceneConverterCollection(),
            middlewareCollection,
            NegativeDeconstructionValidator.Instance,
            ServiceInstanceFactory.Instance,
            _serviceProvider);
    }
}
