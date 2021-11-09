using Kantaiko.Controllers;
using Kantaiko.Controllers.Middleware;
using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Hooks.ApplicationHooks;
using Replikit.Core.Controllers;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views.HookHandlers.ApplicationInitialized;

internal class InitializeViewRequestPipelineHandler : IHookHandler<ApplicationInitializedHook>
{
    private readonly ViewRequestHandlerAccessor _requestHandlerAccessor;
    private readonly IServiceProvider _serviceProvider;

    public InitializeViewRequestPipelineHandler(ViewRequestHandlerAccessor requestHandlerAccessor,
        IServiceProvider serviceProvider)
    {
        _requestHandlerAccessor = requestHandlerAccessor;
        _serviceProvider = serviceProvider;
    }

    public void Handle(ApplicationInitializedHook payload)
    {
        var assemblies = payload.Assemblies.ToArray();

        var controllerCollection = ControllerCollection.FromAssemblies(assemblies);
        var middlewareCollection = MiddlewareCollection.FromAssemblies(assemblies);

        _requestHandlerAccessor.RequestHandler = new RequestHandler<ViewContext>(
            controllerCollection,
            new ViewConverterCollection(),
            middlewareCollection,
            new ViewParameterDeconstructionValidator(),
            ServiceInstanceFactory.Instance,
            _serviceProvider);
    }
}
