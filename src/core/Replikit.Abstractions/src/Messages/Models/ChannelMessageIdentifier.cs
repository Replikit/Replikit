using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// The identifier of the message in some channel.
/// Consists of the non-global channel identifier and the message identifier.
/// </summary>
/// <param name="ChannelId">A non-global channel identifier.</param>
/// <param name="Value">A message identifier.</param>
public readonly record struct ChannelMessageIdentifier(Identifier ChannelId, MessageIdentifier Value)
{
    /// <summary>
    /// The non-global identifier of the channel.
    /// </summary>
    public Identifier ChannelId { get; } = Check.NotDefault(ChannelId);

    /// <summary>
    /// The identifier of the message itself.
    /// </summary>
    public MessageIdentifier Value { get; } = Check.NotDefault(Value);

    /// <summary>
    /// Creates a new instance of <see cref="ChannelMessageIdentifier"/>
    /// with specified channel identifier and identifier of the single message part.
    /// </summary>
    /// <param name="channelId">A non-global channel identifier.</param>
    /// <param name="partIdentifier">A message part identifier.</param>
    public ChannelMessageIdentifier(Identifier channelId, Identifier partIdentifier) :
        this(channelId, new MessageIdentifier(partIdentifier)) { }

    /// <summary>
    /// Creates a new instance of <see cref="ChannelMessageIdentifier"/>
    /// with specified channel identifier and multiple message part identifiers.
    /// </summary>
    /// <param name="channelId">A non-global channel identifier.</param>
    /// <param name="partIdentifiers">Identifiers of the message parts.</param>
    public ChannelMessageIdentifier(Identifier channelId, IEnumerable<Identifier> partIdentifiers) :
        this(channelId, new MessageIdentifier(partIdentifiers)) { }

    /// <summary>
    /// Unwraps the <see cref="MessageIdentifier"/> value from the <see cref="ChannelMessageIdentifier"/>
    /// and ignores the channel identifier.
    /// <br/>
    /// Should be used carefully as it allows to use the identifier for the wrong bot or channel.
    /// </summary>
    /// <param name="identifier">A <see cref="ChannelMessageIdentifier"/> to unwrap.</param>
    /// <returns>The <see cref="MessageIdentifier"/> instance.</returns>
    public static implicit operator MessageIdentifier(ChannelMessageIdentifier identifier) => identifier.Value;

    /// <summary>
    /// Converts the <see cref="GlobalMessageIdentifier"/> to the <see cref="ChannelMessageIdentifier"/>
    /// and ignores the bot identifier of the channel.
    /// <br/>
    /// Should be used carefully as it allows to use the identifier for the wrong bot or channel.
    /// </summary>
    /// <param name="identifier">A <see cref="GlobalMessageIdentifier"/> to convert.</param>
    /// <returns>The <see cref="ChannelMessageIdentifier"/> instance.</returns>
    public static implicit operator ChannelMessageIdentifier(GlobalMessageIdentifier identifier)
    {
        return new ChannelMessageIdentifier(identifier.ChannelId, identifier);
    }
}
