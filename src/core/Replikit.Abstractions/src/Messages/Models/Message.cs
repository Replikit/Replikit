using System.Collections.Immutable;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// Represents a message in some channel.
/// </summary>
public sealed class Message : IHasCustomData
{
    /// <summary>
    /// Creates a new instance of <see cref="Message"/>.
    /// </summary>
    /// <param name="id">An identifier of the message.</param>
    public Message(GlobalMessageIdentifier id)
    {
        Id = Check.NotDefault(id);
    }

    /// <summary>
    /// The global identifier of the message.
    /// </summary>
    public GlobalMessageIdentifier Id { get; init; }

    /// <summary>
    /// The collection of attachments associated with the message.
    /// </summary>
    public IReadOnlyList<Attachment> Attachments { get; init; } = ImmutableArray<Attachment>.Empty;

    /// <summary>
    /// <inheritdoc cref="IHasCustomData.CustomData"/>
    /// </summary>
    public IReadOnlyList<object> CustomData { get; init; } = ImmutableArray<object>.Empty;

    /// <summary>
    /// The identifier of the channel the message was sent in.
    /// <br/>
    /// May be null if the channel is not accessible to the bot.
    /// </summary>
    public GlobalIdentifier? ChannelId { get; init; }

    /// <summary>
    /// The identifier of the account that sent the message.
    /// <br/>
    /// May be null if the account is not accessible to the bot or if the message has not been sent by an account.
    /// </summary>
    public GlobalIdentifier? AccountId { get; init; }

    /// <summary>
    /// The unparsed text of the message.
    /// <br/>
    /// May be null if the message consists of only attachments.
    /// </summary>
    public string? Text { get; init; }

    /// <summary>
    /// The identifier of the message part to which the message is a reply.
    /// <br/>
    /// May be null if the message is not a reply.
    /// </summary>
    public Identifier? ReplyId { get; init; }

    /// <summary>
    /// The message to which the message is a reply.
    /// <br/>
    /// May be null if the message is not a reply or if the message is not accessible to the bot.
    /// </summary>
    public Message? Reply { get; init; }
}
