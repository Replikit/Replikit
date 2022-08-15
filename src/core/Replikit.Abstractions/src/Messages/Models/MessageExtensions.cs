using Replikit.Abstractions.Messages.Exceptions;

namespace Replikit.Abstractions.Messages.Models;

public static class MessageExtensions
{
    public static T GetOriginal<T>(this Message message) where T : class
    {
        try
        {
            return (T) message.Originals[0];
        }
        catch (Exception e)
        {
            throw new OriginalMessageAccessException(e);
        }
    }

    public static IReadOnlyList<T> GetOriginals<T>(this Message message) where T : class
    {
        try
        {
            return message.Originals.Select(x => (T) x).ToArray();
        }
        catch (Exception e)
        {
            throw new OriginalMessageAccessException(e);
        }
    }
}
