using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Common.Models;

/// <summary>
/// The global identifier for a Replikit entity.
/// <br/>
/// In the simplest case, just contains the identifier of the entity itself.
/// May optionally contain the identifier of the platform, where the entity is located.
/// May optionally contain the identifier of the bot, which is used to access the entity.
/// </summary>
public readonly record struct GlobalIdentifier
{
    /// <summary>
    /// Creates a new instance of <see cref="GlobalIdentifier"/>.
    /// </summary>
    /// <param name="value">The identifier of the entity.</param>
    public GlobalIdentifier(Identifier value)
    {
        Value = Check.NotDefault(value);
    }

    /// <summary>
    /// Creates a new instance of <see cref="GlobalIdentifier"/> associated with the specified platform.
    /// </summary>
    /// <param name="platformId">The identifier of the platform.</param>
    /// <param name="value">The identifier of the entity.</param>
    public GlobalIdentifier(string platformId, Identifier value)
    {
        PlatformId = Check.NotNull(platformId);
        Value = Check.NotDefault(value);
    }

    /// <summary>
    /// Creates a new instance of <see cref="GlobalIdentifier"/> associated with the specified bot.
    /// </summary>
    /// <param name="botId">The identifier of the bot.</param>
    /// <param name="value">The identifier of the entity.</param>
    public GlobalIdentifier(BotIdentifier botId, Identifier value)
    {
        Check.NotDefault(botId);
        Value = Check.NotDefault(value);

        PlatformId = botId.PlatformId;
        _botId = botId.Value;
    }

    private readonly Identifier? _botId;

    /// <summary>
    /// The platform identifier.
    /// </summary>
    public string? PlatformId { get; }

    /// <summary>
    /// The identifier of the bot which is used to interact with the entity.
    /// </summary>
    public BotIdentifier? BotId => _botId is null ? null : new BotIdentifier(PlatformId!, _botId.Value);

    /// <summary>
    /// The identifier of the entity itself.
    /// </summary>
    public Identifier Value { get; }

    /// <summary>
    /// Unwraps the <see cref="Identifier"/> value from the <see cref="GlobalIdentifier"/> and ignores the bot identifier.
    /// <br/>
    /// Should be used carefully as it allows to use the identifier for the wrong bot.
    /// </summary>
    /// <param name="identifier">A <see cref="GlobalIdentifier"/> to unwrap.</param>
    /// <returns>The <see cref="Identifier"/> instance.</returns>
    public static implicit operator Identifier(GlobalIdentifier identifier) => identifier.Value;

    public override string ToString()
    {
        if (PlatformId is null)
        {
            return Value.ToString() ?? string.Empty;
        }

        return _botId is null ? $"{PlatformId}:{Value}" : $"{PlatformId}:{_botId}:{Value}";
    }

    /// <summary>
    /// Returns the full identifier string according to the specified <see cref="IdentifierPersistenceMode"/>.
    /// <br/>
    /// If some data is missing, it will be ignored.
    /// </summary>
    /// <param name="persistenceMode">The <see cref="IdentifierPersistenceMode"/> to use.</param>
    /// <returns>The full identifier string.</returns>
    public string ToString(IdentifierPersistenceMode persistenceMode)
    {
        return persistenceMode switch
        {
            IdentifierPersistenceMode.Single => Value.ToString() ?? string.Empty,
            IdentifierPersistenceMode.MultiPlatform => _botId is null
                ? $"{PlatformId}:{Value}"
                : $"{PlatformId}:{_botId}:{Value}",
            IdentifierPersistenceMode.MultiBot => ToString(),
            _ => throw new ArgumentOutOfRangeException(nameof(persistenceMode), persistenceMode, null)
        };
    }

    /// <summary>
    /// Tried to parse the <see cref="GlobalIdentifier"/> from the string.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result of the parsing.</param>
    /// <returns>True if the parsing was successful, false otherwise.</returns>
    public static bool TryParse(string? value, out GlobalIdentifier result)
    {
        if (value is null)
        {
            result = default;
            return false;
        }

        var parts = value.Split(':');

        switch (parts)
        {
            // Full identifier: platform:bot:entity
            case [var platformId, var botId, var id]:
            {
                if (!Identifier.TryParse(botId, out var botIdResult))
                {
                    result = default;
                    return false;
                }

                if (!Identifier.TryParse(id, out var idResult))
                {
                    result = default;
                    return false;
                }

                result = new GlobalIdentifier(new BotIdentifier(platformId, botIdResult), idResult);
                return true;
            }

            // Platform identifier: platform:entity
            case [var platformId, var id]:
            {
                if (!Identifier.TryParse(id, out var idResult))
                {
                    result = default;
                    return false;
                }

                result = new GlobalIdentifier(platformId, idResult);
                return true;
            }

            // Entity identifier: entity
            case [var id]:
            {
                if (!Identifier.TryParse(id, out var idResult))
                {
                    result = default;
                    return false;
                }

                result = new GlobalIdentifier(idResult);
                return true;
            }

            default:
            {
                result = default;
                return false;
            }
        }
    }

    /// <summary>
    /// Parses the <see cref="GlobalIdentifier"/> from the string.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <returns>The parsed <see cref="GlobalIdentifier"/>.</returns>
    /// <exception cref="FormatException">The string is not a valid <see cref="GlobalIdentifier"/>.</exception>
    public static GlobalIdentifier Parse(string? value)
    {
        if (TryParse(value, out var result))
        {
            return result;
        }

        throw new FormatException("Invalid global identifier format");
    }
}
