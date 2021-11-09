using System.Diagnostics;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Adapters.Common.Features;
using Replikit.Adapters.Telegram.Internal;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramEventSource : EventSource
{
    private readonly ITelegramBotClient _backend;
    private readonly TelegramAdapterRepository _repository;
    private readonly TelegramEntityFactory _entityFactory;
    private DateTime _startDate;
    private int _nextUpdateId = -1;

    private CancellationTokenSource? _cancellationTokenSource;
    private Task? _pollingTask;

    public TelegramEventSource(IAdapter adapter, IAdapterEventHandler eventHandler, ITelegramBotClient backend,
        TelegramAdapterRepository repository, TelegramEntityFactory entityFactory) : base(adapter, eventHandler)
    {
        _backend = backend;
        _repository = repository;
        _entityFactory = entityFactory;
    }

    private async Task PollingWorker()
    {
        Debug.Assert(_cancellationTokenSource is not null);

        try
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                var updates = await _backend.GetUpdatesAsync(_nextUpdateId, 100, 60, Array.Empty<UpdateType>(),
                    _cancellationTokenSource.Token);

                if (updates?.Length > 0)
                {
                    HandleUpdates(updates);
                    _nextUpdateId = updates[^1].Id + 1;
                }
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    private async void HandleMediaGroup(IReadOnlyList<Message> messages, bool edited)
    {
        var primary = messages[0];
        var channelInfo = _repository.UpdateChannelInfo(primary.Chat);
        var accountInfo = await _repository.UpdateAccountInfo(primary.From);
        var message = _entityFactory.CreateMessage(messages);
        if (edited) HandleMessageEdited(message, channelInfo, accountInfo);
        else HandleMessageReceived(message, channelInfo, accountInfo);
    }

    private void HandleMessages(IReadOnlyList<Message> messages, bool edited)
    {
        var mediaGroups = messages.GroupBy(x => x.MediaGroupId);
        foreach (var mediaGroup in mediaGroups)
        {
            if (mediaGroup.Key is null)
            {
                foreach (var message in mediaGroup)
                {
                    HandleMediaGroup(new[] { message }, edited);
                }

                continue;
            }

            HandleMediaGroup(mediaGroup.ToArray(), edited);
        }
    }

    private async void HandleUpdates(Update[] updates)
    {
        var receivedMessages = new List<Message>();
        var editedMessages = new List<Message>();

        foreach (var update in updates)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    if (update.Message.Date >= _startDate)
                    {
                        receivedMessages.Add(update.Message);
                    }

                    break;
                }
                case UpdateType.EditedMessage:
                {
                    if (update.EditedMessage.EditDate >= _startDate)
                    {
                        editedMessages.Add(update.EditedMessage);
                    }

                    break;
                }
                case UpdateType.CallbackQuery:
                {
                    var accountInfo = await _repository.UpdateAccountInfo(update.CallbackQuery.From);
                    var message = _entityFactory.CreateMessage(new[] { update.CallbackQuery.Message });
                    HandleButtonPressed(accountInfo, update.CallbackQuery.Data, message);
                    break;
                }
            }
        }

        HandleMessages(receivedMessages, false);
        HandleMessages(editedMessages, true);
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        if (_cancellationTokenSource is not null)
        {
            return Task.CompletedTask;
        }

        _startDate = DateTime.UtcNow;

        _cancellationTokenSource = new CancellationTokenSource();
        _pollingTask = PollingWorker();

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        if (_cancellationTokenSource is null)
        {
            return Task.CompletedTask;
        }

        Debug.Assert(_pollingTask is not null);

        _cancellationTokenSource.Cancel();
        return _pollingTask;
    }
}
