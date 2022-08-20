using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Adapters.Common.Services;

public abstract class AdapterEventSource : AdapterService, IAdapterEventSource
{
    private readonly IAdapterEventDispatcher _eventDispatcher;

    protected AdapterEventSource(IAdapter adapter, IAdapterEventDispatcher eventDispatcher) : base(adapter)
    {
        _eventDispatcher = eventDispatcher;
    }

    protected void HandleMessageReceived(Message message, ChannelInfo channelInfo, AccountInfo accountInfo)
    {
        var messageReceivedEvent = new MessageReceivedEvent(Adapter.BotInfo.Id, message, channelInfo, accountInfo);

        _eventDispatcher.DispatchAsync(messageReceivedEvent, Adapter);
    }

    protected void HandleMessageEdited(Message message, ChannelInfo channelInfo, AccountInfo accountInfo,
        Message? oldMessage = null)
    {
        var messageEditedEvent = new MessageEditedEvent(Adapter.BotInfo.Id, message, channelInfo, accountInfo)
        {
            OldMessage = oldMessage
        };

        _eventDispatcher.DispatchAsync(messageEditedEvent, Adapter);
    }

    protected void HandleMessageDeleted(ChannelInfo channelInfo, AccountInfo accountInfo, Message? message = null)
    {
        var messageDeletedEvent = new MessageDeletedEvent(Adapter.BotInfo.Id, channelInfo, accountInfo)
        {
            Message = message
        };

        _eventDispatcher.DispatchAsync(messageDeletedEvent, Adapter);
    }

    protected void HandleButtonPressed(AccountInfo accountInfo, string? payload = null, Message? message = null,
        Identifier? requestId = null)
    {
        var buttonPressedEvent = new ButtonPressedEvent(Adapter.BotInfo.Id, accountInfo)
        {
            Payload = payload,
            Message = message,
            RequestId = requestId
        };

        _eventDispatcher.DispatchAsync(buttonPressedEvent, Adapter);
    }

    public abstract Task StartListeningAsync(CancellationToken cancellationToken);
    public abstract Task StopListeningAsync(CancellationToken cancellationToken);
}
