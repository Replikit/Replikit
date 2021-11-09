using System.Linq.Expressions;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.EntityCollections;
using Replikit.Extensions.Common.Utils;
using Replikit.Extensions.Common.Views;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

public static class ViewManagerExtensions
{
    public static Task SendView<TView>(this IViewManager viewManager, IMessageCollection messageCollection,
        Expression<Func<TView, Task>> action, CancellationToken cancellationToken = default)
        where TView : View => SendViewInternal<TView>(viewManager, messageCollection, action, cancellationToken);

    public static Task SendView<TView>(this IViewManager viewManager, IMessageCollection messageCollection,
        Expression<Action<TView>> action, CancellationToken cancellationToken = default)
        where TView : View => SendViewInternal<TView>(viewManager, messageCollection, action, cancellationToken);

    private static Task SendViewInternal<TView>(IViewManager viewManager, IMessageCollection messageCollection,
        Expression action,
        CancellationToken cancellationToken) where TView : View
    {
        var (method, parameters) = MethodExpressionTransformer.Transform(action);

        var viewRequest = new ViewRequest(typeof(TView).FullName!,
            method.ToString()!, parameters, messageCollection: messageCollection);

        return viewManager.SendView<TView>(messageCollection, viewRequest, cancellationToken: cancellationToken);
    }

    public static Task<bool> Activate<TView>(this IViewManager viewManager, MessageIdentifier viewId,
        Expression<Action<TView>> action, CancellationToken cancellationToken = default)
        where TView : View => ActivateInternal(viewManager, viewId, action, cancellationToken);

    public static Task<bool> Activate<TView>(this IViewManager viewManager, MessageIdentifier viewId,
        Expression<Func<TView, Task>> action, CancellationToken cancellationToken = default)
        where TView : View => ActivateInternal(viewManager, viewId, action, cancellationToken);

    private static async Task<bool> ActivateInternal(this IViewManager viewManager,
        MessageIdentifier messageId, Expression action, CancellationToken cancellationToken)
    {
        var viewInstance = await viewManager.Storage.GetAsync(messageId, cancellationToken);
        if (viewInstance is null) return false;

        await ActivateInternal(viewManager, viewInstance, action, cancellationToken);
        return true;
    }

    private static Task ActivateInternal(IViewManager viewManager,
        ViewInstance viewInstance, Expression action, CancellationToken cancellationToken)
    {
        var (method, parameters) = MethodExpressionTransformer.Transform(action);

        var viewRequest = new ViewRequest(viewInstance.Type, method.ToString()!, parameters, viewInstance);
        return viewManager.Activate(viewRequest, cancellationToken);
    }
}
