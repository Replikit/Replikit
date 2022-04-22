using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.State;

namespace Replikit.Extensions.Views;

public interface IViewManager
{
    IStateManager StateManager { get; }

    /// <summary>
    /// Sends view to the message collection.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TView"></typeparam>
    /// <returns></returns>
    Task<GlobalMessageIdentifier> SendViewAsync<TView>(ViewRequest request,
        CancellationToken cancellationToken = default) where TView : View;

    /// <summary>
    /// Activates view with specified request.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ActivateAsync(ViewRequest request, CancellationToken cancellationToken = default);
}
