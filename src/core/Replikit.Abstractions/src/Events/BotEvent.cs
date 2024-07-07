using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Events;

/// <summary>
/// <inheritdoc cref="IBotEvent"/>
/// </summary>
public abstract class BotEvent : IBotEvent
{
    private readonly IReadOnlyList<object> _customData = ImmutableArray<object>.Empty;

    /// <summary>
    /// Creates a new instance of <see cref="BotEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    protected BotEvent(BotIdentifier botId)
    {
        BotId = Check.NotDefault(botId);
    }

    /// <summary>
    /// <inheritdoc cref="IBotEvent.BotId"/>
    /// </summary>
    public BotIdentifier BotId { get; }

    /// <summary>
    /// <inheritdoc cref="IBotEvent.CustomData"/>
    /// </summary>
    public IReadOnlyList<object> CustomData
    {
        get => _customData;
        init => _customData = Check.NotNull(value);
    }
}
