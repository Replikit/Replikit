using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Events;

/// <summary>
/// Represents an event received by the bot.
/// </summary>
public interface IBotEvent : IHasCustomData
{
    /// <summary>
    /// The identifier of the bot that received the event.
    /// </summary>
    BotIdentifier BotId { get; }
}
