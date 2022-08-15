using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;
using Replikit.Extensions.State;
using Replikit.Extensions.State.Implementation;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views.Internal;

internal class ViewManager : IViewManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStateStore _stateStore;

    public ViewManager(IServiceProvider serviceProvider, IStateStore stateStore)
    {
        _serviceProvider = serviceProvider;
        _stateStore = stateStore;
    }

    public Dictionary<string, InternalViewHandler> ViewHandlers { get; } = new();

    public async Task<IReadOnlyList<StateItem<ViewState>>> FindByStateAsync<TState>(
        QueryBuilder<StateItem<TState>>? queryBuilder = null,
        CancellationToken cancellationToken = default)
        where TState : class, new()
    {
        var states = await _stateStore.FindStateItemsAsync(queryBuilder, cancellationToken);

        var stateKeys = states.Select(x => x.Key with { Type = typeof(ViewState) }).ToArray();

        return await _stateStore.FindStateItemsAsync<ViewState>(
            q => q.Where(x => stateKeys.Contains(x.Key)), cancellationToken);
    }

    public Task<GlobalMessageIdentifier> SendViewAsync<TView>(GlobalIdentifier channelId,
        CancellationToken cancellationToken = default) where TView : View
    {
        return SendViewCoreAsync<TView>(channelId, null, cancellationToken);
    }

    public Task<GlobalMessageIdentifier> SendViewAsync<TView>(GlobalIdentifier channelId,
        Expression<Action<TView>> action,
        CancellationToken cancellationToken = default) where TView : View
    {
        return SendViewCoreAsync<TView>(channelId, action, cancellationToken);
    }

    public Task<GlobalMessageIdentifier> SendViewAsync<TView>(GlobalIdentifier channelId,
        Expression<Func<TView, object>> action, CancellationToken cancellationToken = default) where TView : View
    {
        return SendViewCoreAsync<TView>(channelId, action, cancellationToken);
    }

    public Task<bool> ActivateAsync<TView>(GlobalMessageIdentifier messageId, Expression<Action<TView>> action,
        CancellationToken cancellationToken = default) where TView : View
    {
        return ActivateViewCoreAsync<TView>(messageId, action, cancellationToken);
    }

    public Task<bool> ActivateAsync<TView>(GlobalMessageIdentifier messageId, Expression<Func<TView, object>> action,
        CancellationToken cancellationToken = default) where TView : View
    {
        return ActivateViewCoreAsync<TView>(messageId, action, cancellationToken);
    }

    public Task<bool> ActivateAsync<TView>(StateItem<ViewState> stateItem, Expression<Action<TView>> action,
        CancellationToken cancellationToken = default) where TView : View
    {
        return ActivateViewCoreAsync<TView>(stateItem, action, cancellationToken);
    }

    public Task<bool> ActivateAsync<TView>(StateItem<ViewState> stateItem, Expression<Func<TView, object>> action,
        CancellationToken cancellationToken = default) where TView : View
    {
        return ActivateViewCoreAsync<TView>(stateItem, action, cancellationToken);
    }

    private async Task<GlobalMessageIdentifier> SendViewCoreAsync<TView>(GlobalIdentifier channelId,
        Expression? action, CancellationToken cancellationToken) where TView : View
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var viewState = scope.ServiceProvider.GetRequiredService<IState<ViewState>>();

        if (!ViewHandlers.TryGetValue(typeof(TView).FullName!, out var viewHandler))
        {
            throw new InvalidOperationException($"No view handler registered for {typeof(TView).FullName}");
        }

        var viewContext = new InternalViewContext(viewHandler.ControllerInfo, action, viewState, channelId);

        var keyFactoryAcceptor = scope.ServiceProvider.GetRequiredService<IStateKeyFactoryAcceptor>();
        keyFactoryAcceptor.SetKeyFactory(new ViewStateKeyFactory(viewContext));

        await viewHandler.Handler.HandleAsync(viewContext, scope.ServiceProvider, cancellationToken);

        return viewContext.ViewMessageId!.Value;
    }

    private async Task<bool> ActivateViewCoreAsync<TView>(GlobalMessageIdentifier messageId, Expression action,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(action);

        await using var scope = _serviceProvider.CreateAsyncScope();
        var stateManager = scope.ServiceProvider.GetRequiredService<IStateManager>();

        var viewState = await stateManager
            .GetViewStateAsync<ViewState>(messageId, cancellationToken: cancellationToken);

        if (viewState.Value.ViewInstance is null)
        {
            return false;
        }

        return await ActivateViewCoreAsync<TView>(scope, viewState, action, cancellationToken);
    }

    private async Task<bool> ActivateViewCoreAsync<TView>(StateItem<ViewState> stateItem, Expression action,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(action);

        await using var scope = _serviceProvider.CreateAsyncScope();

        var stateManager = scope.ServiceProvider.GetRequiredService<IStateManager>();
        var viewState = stateManager.LoadState(stateItem);

        return await ActivateViewCoreAsync<TView>(scope, viewState, action, cancellationToken);
    }

    private async Task<bool> ActivateViewCoreAsync<TView>(IServiceScope scope, IState<ViewState> viewState,
        Expression action,
        CancellationToken cancellationToken)
    {
        if (viewState.Value.ViewInstance is null)
        {
            return false;
        }

        if (!ViewHandlers.TryGetValue(typeof(TView).FullName!, out var viewHandler))
        {
            throw new InvalidOperationException($"No view handler registered for {typeof(TView).FullName}");
        }

        var viewContext = new InternalViewContext(viewHandler.ControllerInfo, action, viewState, null);

        var keyFactoryAcceptor = scope.ServiceProvider.GetRequiredService<IStateKeyFactoryAcceptor>();
        keyFactoryAcceptor.SetKeyFactory(new ViewStateKeyFactory(viewContext));

        await viewHandler.Handler.HandleAsync(viewContext, scope.ServiceProvider, cancellationToken);

        return true;
    }
}
