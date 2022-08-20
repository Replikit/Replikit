namespace Replikit.Abstractions.Channels.Models;

/// <summary>
/// The type of the channel.
/// </summary>
public enum ChannelType : byte
{
    /// <summary>
    /// The type of the channel is not specified or accessible to the bot.
    /// </summary>
    Unknown,

    /// <summary>
    /// The channel is a dialog between the bot and a user.
    /// </summary>
    Direct,

    /// <summary>
    /// The channel is a chat with multiple users.
    /// </summary>
    Group,

    /// <summary>
    /// The channel is a place where people publish posts for large audiences.
    /// </summary>
    PostChannel,

    /// <summary>
    /// The channel is a "virtual" channel containing multiple channels.
    /// <br/>
    /// For example, it can be a discord server or matrix space.
    /// <br/>
    /// Such channels most likely cannot contain any messages, but can contain members and define their permissions.
    /// </summary>
    ChannelGroup
}
