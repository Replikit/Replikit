using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Adapters.Common.Services;

public abstract class EventSource : IEventSource
{
    private readonly IAdapter _adapter;

    private readonly IAdapterEventDispatcher _eventDispatcher;

    protected EventSource(IAdapter adapter, IAdapterEventDispatcher eventDispatcher)
    {
        _adapter = adapter;
        _eventDispatcher = eventDispatcher;
    }

    protected void HandleMessageReceived(Message message, ChannelInfo channelInfo, AccountInfo accountInfo)
    {
        var messageReceivedEvent = new MessageReceivedEvent(_adapter.Id, message, channelInfo, accountInfo);
        _eventDispatcher.DispatchAsync(messageReceivedEvent, _adapter);
    }

    protected void HandleMessageDeleted(Message message, ChannelInfo channelInfo, AccountInfo accountInfo)
    {
        var messageDeletedEvent = new MessageDeletedEvent(_adapter.Id, message, channelInfo, accountInfo);
        _eventDispatcher.DispatchAsync(messageDeletedEvent, _adapter);
    }

    protected void HandleMessageEdited(Message message, ChannelInfo channelInfo, AccountInfo accountInfo)
    {
        var messageEditedEvent = new MessageEditedEvent(_adapter.Id, message, channelInfo, accountInfo);
        _eventDispatcher.DispatchAsync(messageEditedEvent, _adapter);
    }

    protected void HandleButtonPressed(AccountInfo accountInfo, string? data, Message? message = null,
        Identifier? requestId = null)
    {
        var buttonPressedEvent = new ButtonPressedEvent(_adapter.Id, accountInfo, data, message, requestId);
        _eventDispatcher.DispatchAsync(buttonPressedEvent, _adapter);
    }

    public abstract Task StartAsync(CancellationToken cancellationToken);
    public abstract Task StopAsync(CancellationToken cancellationToken);
}
