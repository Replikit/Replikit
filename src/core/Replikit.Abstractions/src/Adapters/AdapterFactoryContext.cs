using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Adapters;

public class AdapterFactoryContext
{
    public AdapterFactoryContext(IAdapterEventDispatcher? eventDispatcher = null,
        IAttachmentCache? attachmentCache = null)
    {
        EventDispatcher = eventDispatcher;
        AttachmentCache = attachmentCache;
    }

    public IAdapterEventDispatcher? EventDispatcher { get; }
    public IAttachmentCache? AttachmentCache { get; }
}
