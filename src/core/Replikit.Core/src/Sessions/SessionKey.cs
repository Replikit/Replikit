using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Core.Sessions;

/// <summary>
/// The static class that provides methods to create session keys.
/// </summary>
public static class SessionKey
{
    /// <summary>
    /// Creates a session key for a channel.
    /// </summary>
    /// <param name="channelId">The channel identifier.</param>
    /// <returns>The session key.</returns>
    public static string ForChannel(GlobalIdentifier channelId)
    {
        return $"channel:{channelId}";
    }

    /// <summary>
    /// Creates a session key for a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The session key.</returns>
    public static string ForAccount(GlobalIdentifier userId)
    {
        return $"account:{userId}";
    }

    /// <summary>
    /// Creates a session key for a message.
    /// </summary>
    /// <param name="messageId">The message identifier.</param>
    /// <returns>The session key.</returns>
    public static string ForMessage(GlobalMessageIdentifier messageId)
    {
        return $"message:{messageId.GetPrimaryId()}";
    }
}
