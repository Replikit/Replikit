using Kantaiko.Properties.Immutable;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.State;
using Replikit.Extensions.State.Context;
using Replikit.Extensions.Views.Exceptions;

namespace Replikit.Extensions.Views.Internal;

internal class ViewManager : IViewManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ViewHandlerAccessor _handlerAccessor;
    private readonly ViewExternalActivationDeterminant _viewExternalActivationDeterminant;

    public IStateManager StateManager { get; }

    public ViewManager(IServiceProvider serviceProvider, ViewHandlerAccessor handlerAccessor,
        IStateManager stateManager,
        ViewExternalActivationDeterminant viewExternalActivationDeterminant)
    {
        _serviceProvider = serviceProvider;
        _handlerAccessor = handlerAccessor;
        StateManager = stateManager;
        _viewExternalActivationDeterminant = viewExternalActivationDeterminant;
    }

    public async Task<GlobalMessageIdentifier> SendViewAsync<TView>(ViewRequest request,
        CancellationToken cancellationToken = default) where TView : View
    {
        var fullName = typeof(TView).FullName!;

        ValidateViewRequest(request, fullName, false);

        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = CreateContext(request, scope.ServiceProvider, cancellationToken);
        await _handlerAccessor.Handler.Handle(context);

        return context.MessageId!.Value;
    }

    public async Task ActivateAsync(ViewRequest request, CancellationToken cancellationToken = default)
    {
        var viewInstance = request.ViewState?.Value.ViewInstance;

        if (viewInstance is null)
        {
            throw new InvalidOperationException("View instance cannot be null for an activation request");
        }

        ValidateViewRequest(request, viewInstance.Type, true);

        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = CreateContext(request, scope.ServiceProvider, cancellationToken);
        await _handlerAccessor.Handler.Handle(context);
    }

    internal static ViewContext CreateContext(ViewRequest request, IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var properties = ImmutablePropertyCollection.Empty
            .Set(new StateContextProperties(ViewStateKeyFactory.Instance));

        return new ViewContext(request, serviceProvider, properties, cancellationToken);
    }

    private void ValidateViewRequest(ViewRequest request, string viewType, bool isExternal)
    {
        var controllerInfo = _handlerAccessor.IntrospectionInfo.Controllers
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
