namespace Replikit.Abstractions.Messages.Services;

[Flags]
public enum MessageServiceFeatures
{
    None = 0,
    Send = 1 << 0,
    Edit = 1 << 1,
    Delete = 1 << 2,
    DeleteMany = 1 << 3,
    Get = 1 << 4,
    GetMany = 1 << 5,
    Find = 1 << 6,
    Pin = 1 << 7,
    Unpin = 1 << 8,
    AnswerInlineButtonRequest = 1 << 9,
    All = ~0
}
