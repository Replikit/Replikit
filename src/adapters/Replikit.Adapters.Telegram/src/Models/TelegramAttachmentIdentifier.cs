using Replikit.Abstractions.Common.Models;

namespace Replikit.Adapters.Telegram.Models;

public record TelegramAttachmentIdentifier(GlobalIdentifier Id, string UploadId) : GlobalIdentifier(Id) { }
