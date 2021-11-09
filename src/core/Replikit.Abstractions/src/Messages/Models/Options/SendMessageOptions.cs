namespace Replikit.Abstractions.Messages.Models.Options;

public class SendMessageOptions : ISplitOptions
{
    public bool SplitMessage { get; init; } = true;
    public bool SplitAttachments { get; init; } = true;
}
