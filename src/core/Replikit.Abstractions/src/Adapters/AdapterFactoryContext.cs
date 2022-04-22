using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Adapters;

public class AdapterFactoryContext
{
    public AdapterFactoryContext(IAdapterEventHandler? eventHandler = null, IAttachmentCache? attachmentCache = null)
    {
        EventHandler = eventHandler;
        AttachmentCache = attachmentCache;
    }

    public IAdapterEventHandler? EventHandler { get; }
    public IAttachmentCache? AttachmentCache { get; }
}
