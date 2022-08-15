using System.Diagnostics;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Adapters.Common.Services;
using Replikit.Adapters.Telegram.Internal;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramEventSource : PollingEventSource<Update>
{
    private readonly ITelegramBotClient _backend;
    private readonly TelegramAdapterRepository _repository;
    private readonly TelegramEntityFactory _entityFactory;
    private DateTime _startDate;
    private int _nextUpdateId = -1;

    public TelegramEventSource(IAdapter adapter, IAdapterEventDispatcher eventDispatcher, ITelegramBotClient backend,
        TelegramAdapterRepository repository, TelegramEntityFactory entityFactory) : base(adapter, eventDispatcher)
    {
        _backend = backend;
        _repository = repository;
        _entityFactory = entityFactory;
    }

    protected override async Task<IEnumerable<Update>?> FetchUpdatesAsync(CancellationToken cancellationToken)
    {
        var updates = await _backend.GetUpdatesAsync(
            _nextUpdateId,
            limit: 100,
            timeout: 60,
            Array.Empty<UpdateType>(),
            cancellationToken
        );

        if (updates.Length == 0) return null;

        _nextUpdateId = updates[^1].Id + 1;

        return updates;
    }

    protected override bool ShouldRetryAfterException(Exception exception)
    {
        return exception is RequestException { Message: "Request timed out" };
    }

    protected override async Task HandleUpdatesAsync(IEnumerable<Update> updates, CancellationToken cancellationToken)
    {
        var receivedMessages = new List<Message>();
        var editedMessages = new List<Message>();

        foreach (var update in updates)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    Debug.Assert(update.Message is not null);

                    if (update.Message.Date >= _startDate)
                    {
                        receivedMessages.Add(update.Message);
                    }

                    break;
                }
                case UpdateType.EditedMessage:
                {
                    Debug.Assert(update.EditedMessage is not null);

                    if (update.EditedMessage.EditDate >= _startDate)
                    {
                        editedMessages.Add(update.EditedMessage);
                    }

                    break;
                }
                case UpdateType.CallbackQuery:
                {
                    Debug.Assert(update.CallbackQuery is not null);

                    var accountInfo = await _repository.UpdateAccountInfo(update.CallbackQuery.From, cancellationToken);

                    var message = update.CallbackQuery.Message is not null
                        ? _entityFactory.CreateMessage(new[] { update.CallbackQuery.Message })
                        : null;

                    HandleButtonPressed(accountInfo, update.CallbackQuery.Data, message, update.CallbackQuery.Id);
                    break;
                }
            }
        }

        HandleMessages(receivedMessages, false);
        HandleMessages(editedMessages, true);
    }

    private async void HandleMediaGroup(IReadOnlyList<Message> messages, bool edited)
    {
        var primary = messages[0];

        Debug.Assert(primary.From is not null);

        var channelInfo = _repository.UpdateChannelInfo(primary.Chat);
        var accountInfo = await _repository.UpdateAccountInfo(primary.From);

        var message = _entityFactory.CreateMessage(messages);

        if (edited) HandleMessageEdited(message, channelInfo, accountInfo);
        else HandleMessageReceived(message, channelInfo, accountInfo);
    }

    private void HandleMessages(IEnumerable<Message> messages, bool edited)
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

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await base.StartAsync(cancellationToken);

        _startDate = DateTime.UtcNow;
    }
}
