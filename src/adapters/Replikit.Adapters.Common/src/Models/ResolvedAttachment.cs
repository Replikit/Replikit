using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Adapters.Common.Models;

public record ResolvedAttachment<TSource>(Attachment Original, TSource Source);
