using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Messages.Events;

public class ButtonPressedEvent : AccountEvent
{
    public string? Data { get; }
    public Message? Message { get; }
    public Identifier? RequestId { get; }

    public ButtonPressedEvent(AdapterIdentifier adapterId, AccountInfo account, string? data = null,
        Message? message = null, Identifier? requestId = null) :
        base(adapterId, account)
    {
        Data = data;
        Message = message;
        RequestId = requestId;
    }
}
