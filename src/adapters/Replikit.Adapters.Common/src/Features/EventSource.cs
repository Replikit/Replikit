﻿using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Adapters.Common.Features;

public abstract class EventSource : IEventSource
{
    private readonly IAdapter _adapter;

    private IAdapterEventHandler _eventHandler;

    protected EventSource(IAdapter adapter, IAdapterEventHandler eventHandler)
    {
        _adapter = adapter;
        _eventHandler = eventHandler;
    }

    protected void HandleMessageReceived(Message message, ChannelInfo channelInfo, AccountInfo accountInfo)
    {
        var messageReceivedEvent = new MessageReceivedEvent(_adapter.Id, message, channelInfo, accountInfo);
        _eventHandler.HandleAsync(messageReceivedEvent, _adapter);
    }

    protected void HandleMessageDeleted(Message message, ChannelInfo channelInfo, AccountInfo accountInfo)
    {
        var messageDeletedEvent = new MessageDeletedEvent(_adapter.Id, message, channelInfo, accountInfo);
        _eventHandler.HandleAsync(messageDeletedEvent, _adapter);
    }

    protected void HandleMessageEdited(Message message, ChannelInfo channelInfo, AccountInfo accountInfo)
    {
        var messageEditedEvent = new MessageEditedEvent(_adapter.Id, message, channelInfo, accountInfo);
        _eventHandler.HandleAsync(messageEditedEvent, _adapter);
    }

    protected void HandleButtonPressed(AccountInfo accountInfo, string data, Message? message = null)
    {
        var buttonPressedEvent = new ButtonPressedEvent(_adapter.Id, accountInfo, data, message);
        _eventHandler.HandleAsync(buttonPressedEvent, _adapter);
    }

    public void SetHandler(IAdapterEventHandler eventHandler)
    {
        ArgumentNullException.ThrowIfNull(eventHandler);

        _eventHandler = eventHandler;
    }

    public abstract Task StartAsync(CancellationToken cancellationToken);
    public abstract Task StopAsync(CancellationToken cancellationToken);
}
