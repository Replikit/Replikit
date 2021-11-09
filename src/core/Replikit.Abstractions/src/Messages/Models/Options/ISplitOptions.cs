namespace Replikit.Abstractions.Messages.Models.Options;

public interface ISplitOptions
{
    /// <summary>
    /// Allow splitting of message with attachments if they cannot be sent in one message.
    /// </summary>
    public bool SplitMessage { get; init; }

    /// <summary>
    /// Allow splitting attachments by messages if they exceed the count limit.
    /// </summary>
    public bool SplitAttachments { get; init; }
}
