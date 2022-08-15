using System.Reflection;
using Kantaiko.Controllers;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection.Factory;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.ExecutionHandlers;
using Replikit.Extensions.Views.ExecutionHandlers;

namespace Replikit.Extensions.Views.Internal;

internal class ViewHandlerFactory
{
    private readonly ViewManager _viewManager;
    private readonly IServiceProvider _serviceProvider;

    public ViewHandlerFactory(ViewManager viewManager, IServiceProvider serviceProvider)
    {
        _viewManager = viewManager;
        _serviceProvider = serviceProvider;
    }

    public void InitializeHandlers(Assembly assembly)
    {
        var introspectionBuilder = new IntrospectionBuilder<InternalViewContext>();

        var handlers = new HandlerCollection<InternalViewContext>
        {
            new ResolveEndpointAndInstantiateViewHandler(),
            new InvokeHandlerInstanceInterceptorsHandler<InternalViewContext>(),
            new LoadViewActionParametersHandler(),
            new InvokeOptionalEndpointHandler(),
            ActivatorUtilities.CreateInstance<UpdateViewAndSaveStateHandler>(_serviceProvider)
        };

        handlers.AddRequestCompletion();

        var lookupTypes = assembly.GetTypes();
        var introspectionInfo = introspectionBuilder.CreateIntrospectionInfo(lookupTypes);

        var viewHandler = ControllerHandlerFactory.CreateControllerHandler(introspectionInfo, handlers);

        foreach (var controllerInfo in introspectionInfo.Controllers)
        {
            _viewManager.ViewHandlers[controllerInfo.Type.FullName!] =
                new InternalViewHandler(controllerInfo, viewHandler);
        }
    }
}
