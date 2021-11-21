using Replikit.Core.EntityCollections;
using Replikit.Extensions.Common.Views;

namespace Replikit.Extensions.Views;

public interface IViewManager
{
    IViewStorage Storage { get; }

    /// <summary>
    /// Sends view to the message collection.
    /// </summary>
    /// <param name="messageCollection"></param>
    /// <param name="request"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TView"></typeparam>
    /// <returns></returns>
    Task SendViewAsync<TView>(IMessageCollection messageCollection, ViewRequest? request = null, bool autoSave = true,
        CancellationToken cancellationToken = default)
        where TView : View;

    /// <summary>
    /// Activates view with specified request.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ActivateAsync(ViewRequest request, CancellationToken cancellationToken = default);
}
