using System.Linq.Expressions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;
using Replikit.Extensions.Views.Models;

namespace Replikit.Extensions.Views;

public interface IViewManager
{
    /// <summary>
    /// Finds state items of all views associated with the specified state matched by the specified predicate.
    /// </summary>
    /// <param name="queryBuilder"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TState"></typeparam>
    /// <returns></returns>
    Task<IReadOnlyList<StateItem<ViewState>>> FindByStateAsync<TState>(
        QueryBuilder<StateItem<TState>>? queryBuilder = null,
        CancellationToken cancellationToken = default)
        where TState : class, new();

    /// <summary>
    /// Sends view to the message collection.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TView"></typeparam>
    /// <returns></returns>
    Task<GlobalMessageIdentifier> SendViewAsync<TView>(GlobalIdentifier channelId,
        CancellationToken cancellationToken = default) where TView : View;

    /// <summary>
    /// Sends view to the message collection.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TView"></typeparam>
    /// <returns></returns>
    Task<GlobalMessageIdentifier> SendViewAsync<TView>(GlobalIdentifier channelId, Expression<Action<TView>> action,
        CancellationToken cancellationToken = default) where TView : View;

    /// <summary>
    /// Sends view to the message collection.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TView"></typeparam>
    /// <returns></returns>
    Task<GlobalMessageIdentifier> SendViewAsync<TView>(GlobalIdentifier channelId,
        Expression<Func<TView, object>> action,
        CancellationToken cancellationToken = default) where TView : View;

    /// <summary>
    /// Activates view with specified action.
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ActivateAsync<TView>(GlobalMessageIdentifier messageId, Expression<Action<TView>> action,
        CancellationToken cancellationToken = default) where TView : View;

    /// <summary>
    /// Activates view with specified action.
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ActivateAsync<TView>(GlobalMessageIdentifier messageId, Expression<Func<TView, object>> action,
        CancellationToken cancellationToken = default) where TView : View;

    /// <summary>
    /// Activates view with specified action.
    /// </summary>
    /// <param name="stateItem"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ActivateAsync<TView>(StateItem<ViewState> stateItem, Expression<Action<TView>> action,
        CancellationToken cancellationToken = default) where TView : View;

    /// <summary>
    /// Activates view with specified action.
    /// </summary>
    /// <param name="stateItem"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ActivateAsync<TView>(StateItem<ViewState> stateItem, Expression<Func<TView, object>> action,
        CancellationToken cancellationToken = default) where TView : View;
}
