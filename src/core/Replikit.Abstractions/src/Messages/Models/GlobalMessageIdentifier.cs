using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// The global identifier of the message.
/// Consists of the global channel identifier and the message identifier.
/// <param name="ChannelId">A global channel identifier.</param>
/// <param name="Value">An identifier of the message.</param>
/// </summary>
public readonly record struct GlobalMessageIdentifier(GlobalIdentifier ChannelId, MessageIdentifier Value)
{
    /// <summary>
    /// The global identifier of the channel where the message is located.
    /// </summary>
    public GlobalIdentifier ChannelId { get; } = Check.NotDefault(ChannelId);

    /// <summary>
    /// The identifier of the message itself.
    /// </summary>
    public MessageIdentifier Value { get; } = Check.NotDefault(Value);

    /// <summary>
    /// Creates a new instance of <see cref="GlobalMessageIdentifier"/>
    /// with the specified channel identifier and single message part identifier.
    /// </summary>
    /// <param name="channelId">A global identifier of the channel.</param>
    /// <param name="partIdentifier">An identifier of the message part.</param>
    public GlobalMessageIdentifier(GlobalIdentifier channelId, Identifier partIdentifier) :
        this(channelId, new MessageIdentifier(partIdentifier)) { }

    /// <summary>
    /// Creates a new instance of <see cref="GlobalMessageIdentifier"/>
    /// with the specified channel identifier and multiple message part identifiers.
    /// </summary>
    /// <param name="channelId">A global identifier of the channel.</param>
    /// <param name="partIdentifiers">Identifiers of the message parts.</param>
    public GlobalMessageIdentifier(GlobalIdentifier channelId, IEnumerable<Identifier> partIdentifiers) :
        this(channelId, new MessageIdentifier(partIdentifiers)) { }

    /// <summary>
    /// Unwraps the <see cref="MessageIdentifier"/> value from the <see cref="GlobalMessageIdentifier"/>
    /// and ignores the channel identifier.
    /// <br/>
    /// Should be used carefully as it allows to use the identifier for the wrong bot or channel.
    /// </summary>
    /// <param name="identifier">A <see cref="GlobalMessageIdentifier"/> to unwrap.</param>
    /// <returns>The <see cref="MessageIdentifier"/> instance.</returns>
    public static implicit operator MessageIdentifier(GlobalMessageIdentifier identifier) => identifier.Value;

    public override string ToString() => $"{ChannelId}:{Value}";

    /// <summary>
    /// Gets the primary identifier of the message.
    /// </summary>
    /// <returns>The primary identifier of the message.</returns>
    public string GetPrimaryId() => $"{ChannelId}:{Value.GetPrimaryId()}";

    /// <summary>
    /// Try to parse the string representation of the global message identifier.
    /// </summary>
    /// <param name="value">A string representation of the global message identifier.</param>
    /// <param name="result">The parsed global message identifier.</param>
    /// <returns>True if the parsing was successful, otherwise false.</returns>
    public static bool TryParse(string? value, out GlobalMessageIdentifier result)
    {
        if (value is null)
        {
            result = default;
            return false;
        }

        var parts = value.Split(':', 2);

        if (parts.Length != 2)
        {
            result = default;
            return false;
        }

        if (!GlobalIdentifier.TryParse(parts[0], out var channelId))
        {
            result = default;
            return false;
        }

        if (!MessageIdentifier.TryParse(parts[1], out var messageIdentifier))
        {
            result = default;
            return false;
        }

        result = new GlobalMessageIdentifier(channelId, messageIdentifier);
        return true;
    }
}
