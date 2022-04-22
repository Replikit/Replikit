using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.State.Implementation;

public readonly record struct PartialStateKey(
    StateType? StateType = null,
    AdapterIdentifier? AdapterId = null,
    Identifier? ChannelId = null,
    Identifier? AccountId = null,
    MessageIdentifier? MessageId = null,
    Type? Type = null
);
