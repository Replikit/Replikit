using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Adapters.Common.Models;

public record ResolvedAttachment<TSource>(OutAttachment Original, TSource Source);
