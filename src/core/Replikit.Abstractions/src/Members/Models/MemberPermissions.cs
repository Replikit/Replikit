namespace Replikit.Abstractions.Members.Models;

/// <summary>
/// The permissions of an account in some channel.
/// </summary>
[Flags]
public enum MemberPermissions
{
    /// <summary>
    /// The user has no permissions in the channel.
    /// </summary>
    None = 0,

    /// <summary>
    /// The user can send messages in the channel.
    /// </summary>
    SendMessages = 1 << 0,

    /// <summary>
    /// The user can edit messages in the channel.
    /// </summary>
    EditMessages = 1 << 1,

    /// <summary>
    /// The user can delete it's messages in the channel.
    /// </summary>
    DeleteMessages = 1 << 2,

    /// <summary>
    /// The user can delete any messages in the channel.
    /// </summary>
    DeleteAnyMessages = 1 << 3,

    /// <summary>
    /// The user can manage the permissions of other accounts in the channel.
    /// </summary>
    ManagePermissions = 1 << 4,

    /// <summary>
    /// The user can change title of the channel.
    /// </summary>
    ChangeChannelTitle = 1 << 5,

    /// <summary>
    /// The user can add other users to the channel.
    /// </summary>
    AddUsers = 1 << 6,

    /// <summary>
    /// The user can remove other users from the channel.
    /// </summary>
    RemoveUsers = 1 << 7,

    /// <summary>
    /// The user have all permissions in the channel.
    /// </summary>
    All = ~0
}
