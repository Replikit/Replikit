using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Adapters.Factory;

/// <summary>
/// The context used to create adapters.
/// </summary>
public class AdapterFactoryContext
{
    /// <summary>
    /// Creates a new instance of <see cref="AdapterFactoryContext"/>.
    /// </summary>
    /// <param name="eventDispatcher">An event dispatcher.</param>
    /// <param name="attachmentCache">An attachment cache.</param>
    public AdapterFactoryContext(IAdapterEventDispatcher? eventDispatcher = null,
        IAttachmentCache? attachmentCache = null)
    {
        EventDispatcher = eventDispatcher;
        AttachmentCache = attachmentCache;
    }

    /// <summary>
    /// The event dispatcher to provide to the adapter.
    /// </summary>
    public IAdapterEventDispatcher? EventDispatcher { get; }

    /// <summary>
    /// The attachment cache to provide to the adapter.
    /// </summary>
    public IAttachmentCache? AttachmentCache { get; }
}
