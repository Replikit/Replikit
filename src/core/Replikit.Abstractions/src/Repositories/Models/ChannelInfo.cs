using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Repositories.Models;

public sealed record ChannelInfo(
    GlobalIdentifier Id,
    ChannelType Type = ChannelType.Unknown,
    string? Title = null,
    GlobalIdentifier? ParentId = null
);
