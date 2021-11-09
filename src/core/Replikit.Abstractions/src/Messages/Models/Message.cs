using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Exceptions;

namespace Replikit.Abstractions.Messages.Models;

public record Message(
    GlobalMessageIdentifier Id,
    IReadOnlyList<Attachment> Attachments,
    IReadOnlyList<object> Originals,
    GlobalIdentifier? ChannelId = null,
    GlobalIdentifier? AccountId = null,
    string? Text = null,
    Message? Reply = null)
{
    public T GetOriginal<T>() where T : class
    {
        try
        {
            return (T) Originals[0];
        }
        catch (Exception e)
        {
            throw new OriginalMessageAccessException(e);
        }
    }

    public IReadOnlyList<T> GetOriginals<T>() where T : class
    {
        try
        {
            return Originals.Select(x => (T) x).ToArray();
        }
        catch (Exception e)
        {
            throw new OriginalMessageAccessException(e);
        }
    }
}
