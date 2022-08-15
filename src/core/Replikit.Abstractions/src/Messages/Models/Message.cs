using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// Represents the message in a channel.
/// </summary>
public record Message(
    GlobalMessageIdentifier Id,
    IReadOnlyList<Attachment> Attachments,
    IReadOnlyList<object> Originals,
    GlobalIdentifier? ChannelId = null,
    GlobalIdentifier? AccountId = null,
    string? Text = null,
    // Identifier? ReplyId = null,
    Message? Reply = null
);
