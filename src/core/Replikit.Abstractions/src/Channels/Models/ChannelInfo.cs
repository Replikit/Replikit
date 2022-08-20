using System.Collections.Immutable;
using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Channels.Models;

/// <summary>
/// Represents a channel on some platform (messenger or social network).
/// </summary>
public sealed class ChannelInfo : IHasCustomData
{
    public ChannelInfo(GlobalIdentifier id, ChannelType type)
    {
        Id = Check.NotDefault(id);
        Type = type;
    }

    /// <summary>
    /// The global identifier of the channel.
    /// </summary>
    public GlobalIdentifier Id { get; }

    /// <summary>
    /// The type of the channel.
    /// </summary>
    public ChannelType Type { get; }

    /// <summary>
    /// The title of the channel.
    /// <br/>
    /// May be null if the channel does not have a title or it is not available to the bot.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// The identifier of the parent channel.
    /// </summary>
    public GlobalIdentifier? ParentId { get; init; }

    public IReadOnlyList<object> CustomData { get; init; } = ImmutableArray<object>.Empty;
}
