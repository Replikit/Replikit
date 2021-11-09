using Kantaiko.Controllers;
using Kantaiko.Controllers.Converters;
using Kantaiko.Controllers.Middleware;
using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Hooks.ApplicationHooks;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Controllers.Internal;
using Replikit.Core.Handlers;

namespace Replikit.Core.Controllers.HookHandlers.ApplicationInitialized;

internal class InitializeRequestPipelineHandler : IHookHandler<ApplicationInitializedHook>
{
    private readonly RequestHandlerAccessor _requestHandlerAccessor;
    private readonly IServiceProvider _serviceProvider;

    public InitializeRequestPipelineHandler(RequestHandlerAccessor requestHandlerAccessor,
        IServiceProvider serviceProvider)
    {
        _requestHandlerAccessor = requestHandlerAccessor;
        _serviceProvider = serviceProvider;
    }

    public void Handle(ApplicationInitializedHook payload)
    {
        var assemblies = payload.Assemblies.ToArray();

        var controllerCollection = ControllerCollection.FromAssemblies(assemblies);
        var converterCollection = ConverterCollection.FromAssemblies(assemblies);
        var middlewareCollection = MiddlewareCollection.FromAssemblies(assemblies);

        _requestHandlerAccessor.RequestHandler = new RequestHandler<IEventContext<MessageReceivedEvent>>(
            controllerCollection,
            converterCollection,
            middlewareCollection,
            instanceFactory: ServiceInstanceFactory.Instance,
            serviceProvider: _serviceProvider);
    }
}
