using System.Collections.Immutable;
using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Members.Models;

/// <summary>
/// Represents a member of the channel.
/// </summary>
public sealed class MemberInfo : IHasCustomData
{
    /// <summary>
    /// Represents a member of some channel.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="account">An account of the member.</param>
    public MemberInfo(GlobalIdentifier channelId, AccountInfo account)
    {
        ChannelId = Check.NotDefault(channelId);
        Account = Check.NotNull(account);
    }

    /// <summary>
    /// The identifier of the channel.
    /// </summary>
    public GlobalIdentifier ChannelId { get; }

    /// <summary>
    /// The account of the member.
    /// </summary>
    public AccountInfo Account { get; }

    /// <summary>
    /// The permissions of the member in the channel.
    /// </summary>
    public MemberPermissions Permissions { get; init; }

    /// <summary>
    /// <inheritdoc cref="IHasCustomData.CustomData"/>
    /// </summary>
    public IReadOnlyList<object> CustomData { get; init; } = ImmutableArray<object>.Empty;
}
