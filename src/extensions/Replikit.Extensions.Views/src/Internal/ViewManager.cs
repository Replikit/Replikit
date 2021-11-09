using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.EntityCollections;
using Replikit.Extensions.Common.Views;
using Replikit.Extensions.Views.Exceptions;

namespace Replikit.Extensions.Views.Internal;

internal class ViewManager : IViewManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ViewRequestHandlerAccessor _requestHandlerAccessor;
    private readonly ViewExternalActivationDeterminant _viewExternalActivationDeterminant;

    public IViewStorage Storage { get; }

    public ViewManager(IServiceProvider serviceProvider, ViewRequestHandlerAccessor requestHandlerAccessor,
        IViewStorage viewStorage,
        ViewExternalActivationDeterminant viewExternalActivationDeterminant)
    {
        _serviceProvider = serviceProvider;
        _requestHandlerAccessor = requestHandlerAccessor;
        Storage = viewStorage;
        _viewExternalActivationDeterminant = viewExternalActivationDeterminant;
    }

    public async Task SendView<TView>(IMessageCollection messageCollection, ViewRequest? request = null,
        bool autoSave = true, CancellationToken cancellationToken = default)
        where TView : View
    {
        var fullName = typeof(TView).FullName!;

        request ??= new ViewRequest(fullName, "Void Init()",
            Array.Empty<object>(),
            messageCollection: messageCollection,
            autoSave: autoSave);

        ValidateViewRequest(request, fullName, false);

        var context = CreateContext(request, cancellationToken);
        await _requestHandlerAccessor.RequestHandler.HandleAsync(context, _serviceProvider, cancellationToken);
    }

    public async Task Activate(ViewRequest request, CancellationToken cancellationToken = default)
    {
        var viewInstance = request.ViewInstance;

        if (viewInstance is null)
        {
            throw new InvalidOperationException("View instance cannot be null for an activation request");
        }

        ValidateViewRequest(request, viewInstance.Type, true);

        var context = CreateContext(request, cancellationToken);
        await _requestHandlerAccessor.RequestHandler.HandleAsync(context, _serviceProvider, cancellationToken);
    }

    private ViewContext CreateContext(ViewRequest request, CancellationToken cancellationToken)
    {
        var context = new ViewContext(request, _serviceProvider, cancellationToken);
        _serviceProvider.GetRequiredService<ViewRequestContextAccessor>().Context = context;

        return context;
    }

    private void ValidateViewRequest(ViewRequest request, string viewType, bool isExternal)
    {
        var controllerInfo = _requestHandlerAccessor.RequestHandler.Info.Controllers
            .FirstOrDefault(x => x.Type.FullName == viewType);

        if (controllerInfo is null)
        {
            throw new ViewNotRegisteredException(viewType);
        }

        var endpoint = controllerInfo.Endpoints.FirstOrDefault(x => x.MethodInfo.ToString() == request.Method);

        if (endpoint is null)
        {
            throw new ViewMethodNotFoundException(controllerInfo.Type.Name, request.Method);
        }

        if (isExternal && !_viewExternalActivationDeterminant.IsExternalActivationAllowed(endpoint))
        {
            throw new ExternalActivationNotAllowedException(endpoint);
        }
    }
}
