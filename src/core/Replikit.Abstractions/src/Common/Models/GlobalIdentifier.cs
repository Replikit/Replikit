using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Common.Models;

/// <summary>
/// The global identifier for a Replikit entity.
/// <br/>
/// Consists of the <see cref="BotId"/> which is used to interact with the entity and the <see cref="Value"/> itself.
/// </summary>
/// <param name="BotId">A <see cref="BotIdentifier"/>.</param>
/// <param name="Value">An <see cref="Identifier"/>.</param>
public readonly record struct GlobalIdentifier(BotIdentifier BotId, Identifier Value)
{
    /// <summary>
    /// The identifier of the bot which is used to interact with the entity.
    /// </summary>
    public BotIdentifier BotId { get; } = Check.NotDefault(BotId);

    /// <summary>
    /// The identifier of the entity itself.
    /// </summary>
    public Identifier Value { get; } = Check.NotDefault(Value);

    /// <summary>
    /// Unwraps the <see cref="Identifier"/> value from the <see cref="GlobalIdentifier"/> and ignores the bot identifier.
    /// <br/>
    /// Should be used carefully as it allows to use the identifier for the wrong bot.
    /// </summary>
    /// <param name="identifier">A <see cref="GlobalIdentifier"/> to unwrap.</param>
    /// <returns>The <see cref="Identifier"/> instance.</returns>
    public static implicit operator Identifier(GlobalIdentifier identifier) => identifier.Value;
}
