using System.Linq.Expressions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Utils;
using Replikit.Extensions.State;
using Replikit.Extensions.Storage.Models;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views;

public static class ViewManagerExtensions
{
    public static Task<GlobalMessageIdentifier> SendViewAsync<TView>(this IViewManager viewManager,
        GlobalIdentifier channelId, CancellationToken cancellationToken = default)
        where TView : View
    {
        var viewRequest = new ViewRequest(typeof(TView).FullName!,
            "Void Init()", Array.Empty<DynamicValue>(), channelId: channelId);

        return viewManager.SendViewAsync<TView>(viewRequest, cancellationToken);
    }

    public static Task<GlobalMessageIdentifier> SendViewAsync<TView>(this IViewManager viewManager,
        GlobalIdentifier channelId,
        Expression<Func<TView, Task>> action, CancellationToken cancellationToken = default)
        where TView : View => SendViewInternal<TView>(viewManager, channelId, action, cancellationToken);

    public static Task<GlobalMessageIdentifier> SendViewAsync<TView>(this IViewManager viewManager,
        GlobalIdentifier channelId,
        Expression<Action<TView>> action, CancellationToken cancellationToken = default)
        where TView : View => SendViewInternal<TView>(viewManager, channelId, action, cancellationToken);

    private static Task<GlobalMessageIdentifier> SendViewInternal<TView>(IViewManager viewManager,
        GlobalIdentifier channelId, Expression action, CancellationToken cancellationToken) where TView : View
    {
        var (method, parameters) = MethodExpressionTransformer.Transform(action);

        var dynamicParameters = parameters.Select(x => new DynamicValue(x)).ToArray();

        var viewRequest = new ViewRequest(typeof(TView).FullName!,
            method.ToString()!, dynamicParameters, channelId: channelId);

        return viewManager.SendViewAsync<TView>(viewRequest, cancellationToken);
    }

    public static Task<bool> TryActivateAsync<TView>(this IViewManager viewManager, GlobalMessageIdentifier messageId,
        Expression<Action<TView>> action, CancellationToken cancellationToken = default)
        where TView : View => ActivateInternalAsync(viewManager, messageId, action, cancellationToken);

    public static Task<bool> TryActivateAsync<TView>(this IViewManager viewManager, GlobalMessageIdentifier messageId,
        Expression<Func<TView, Task>> action, CancellationToken cancellationToken = default)
        where TView : View => ActivateInternalAsync(viewManager, messageId, action, cancellationToken);

    public static Task ActivateAsync<TView>(this IViewManager viewManager, IState<ViewState> viewState,
        Expression<Action<TView>> action, CancellationToken cancellationToken = default)
        where TView : View => ActivateInternalAsync(viewManager, viewState, action, cancellationToken);

    public static Task ActivateAsync<TView>(this IViewManager viewManager, IState<ViewState> viewState,
        Expression<Func<TView, Task>> action, CancellationToken cancellationToken = default)
        where TView : View => ActivateInternalAsync(viewManager, viewState, action, cancellationToken);

    private static async Task<bool> ActivateInternalAsync(this IViewManager viewManager,
        GlobalMessageIdentifier messageId, Expression action, CancellationToken cancellationToken)
    {
        var viewState = await viewManager.StateManager.GetViewStateAsync<ViewState>(messageId, cancellationToken);
        if (viewState.Value.ViewInstance is null) return false;

        await ActivateInternalAsync(viewManager, viewState, action, cancellationToken);
        return true;
    }

    private static Task ActivateInternalAsync(IViewManager viewManager,
        IState<ViewState> viewState, Expression action, CancellationToken cancellationToken)
    {
        var (method, parameters) = MethodExpressionTransformer.Transform(action);

        var dynamicParameters = parameters.Select(x => new DynamicValue(x)).ToArray();

        var viewRequest = new ViewRequest(viewState.Value.ViewInstance!.Type,
            method.ToString()!, dynamicParameters, viewState);

        return viewManager.ActivateAsync(viewRequest, cancellationToken);
    }
}
