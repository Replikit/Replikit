using Replikit.Abstractions.Accounts.Events;
using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Events;

/// <summary>
/// Represents an event occurred when someone pressed an inline button.
/// </summary>
public class ButtonPressedEvent : AccountEvent
{
    private readonly Identifier? _requestId;

    /// <summary>
    /// Creates a new instance of <see cref="ButtonPressedEvent"/>.
    /// </summary>
    /// <param name="botId">A bot identifier.</param>
    /// <param name="account">An account.</param>
    public ButtonPressedEvent(BotIdentifier botId, AccountInfo account) : base(botId, account) { }

    /// <summary>
    /// The unique request identifier.
    /// <br/>
    /// In some platforms can be used to respond to the request.
    /// <br/>
    /// May be null if the platform doesn't assign identifiers to button press requests.
    /// </summary>
    public Identifier? RequestId
    {
        get => _requestId;
        init => _requestId = Check.NullOrNotDefault(value);
    }

    /// <summary>
    /// The string-based payload of the button that was pressed.
    /// <br/>
    /// May be null if the button has no payload.
    /// </summary>
    public string? Payload { get; init; }

    /// <summary>
    /// The message containing the button that was pressed.
    /// <br/>
    /// May be null if the message is not accessible to the bot.
    /// </summary>
    public Message? Message { get; init; }
}
