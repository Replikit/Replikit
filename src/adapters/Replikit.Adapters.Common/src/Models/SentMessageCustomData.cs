using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Adapters.Common.Models;

internal record SentMessageCustomData(IReadOnlyList<SentAttachment> SentAttachments);
