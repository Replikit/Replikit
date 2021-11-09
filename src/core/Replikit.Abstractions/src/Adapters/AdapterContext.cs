using Replikit.Abstractions.Attachments.Features;
using Replikit.Abstractions.Events;

namespace Replikit.Abstractions.Adapters;

public class AdapterContext
{
    public AdapterContext(IAdapterEventHandler? eventHandler = null, IAttachmentCache? attachmentCache = null)
    {
        EventHandler = eventHandler;
        AttachmentCache = attachmentCache;
    }

    public IAdapterEventHandler? EventHandler { get; }
    public IAttachmentCache? AttachmentCache { get; }
}
