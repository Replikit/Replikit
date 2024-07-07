using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Events;

/// <summary>
/// Represents a bot event not described by any Replikit event.
/// A client code can access the original event by <see cref="BotEvent.CustomData"/> property.
/// </summary>
public class UnknownBotEvent : BotEvent
{
    /// <summary>
    /// Creates a new instance of <see cref="UnknownBotEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    public UnknownBotEvent(BotIdentifier botId) : base(botId) { }
}
