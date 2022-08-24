using System.Diagnostics;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Adapters.Common.Utils;
using Replikit.Adapters.Telegram.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramMessage = Telegram.Bot.Types.Message;

namespace Replikit.Adapters.Telegram.Internal;

internal class TelegramMessageBuilder
{
    private readonly TelegramEntityFactory _telegramEntityFactory;
    private readonly SentMessageBuilder _messageBuilder;

    public TelegramMessageBuilder(BotIdentifier botId, Identifier channelId,
        TelegramEntityFactory telegramEntityFactory, OutMessage message)
    {
        _telegramEntityFactory = telegramEntityFactory;

        ReplyToMessageId = (int?) message.Reply ?? 0;
        ReplyMarkup = CreateReplyMarkup(message);

        _messageBuilder = new SentMessageBuilder(botId, channelId);
    }

    public int ReplyToMessageId { get; private set; }
    public IReplyMarkup? ReplyMarkup { get; private set; }

    private void ResetReply()
    {
        if (ReplyToMessageId is 0) return;

        ReplyToMessageId = 0;
        ReplyMarkup = null;
    }

    public void ApplyResult(TelegramMessage telegramMessage, OutAttachment? outAttachment = null)
    {
        var attachment = _telegramEntityFactory.ExtractAttachment(telegramMessage);

        _messageBuilder.AddPartIdentifier(telegramMessage.MessageId);
        _messageBuilder.AddCustomData(telegramMessage);

        if (telegramMessage.ReplyToMessage is not null)
        {
            _messageBuilder.SetReplyId(telegramMessage.ReplyToMessage.MessageId);
        }

        if (attachment is not null)
        {
            Debug.Assert(outAttachment is not null);

            _messageBuilder.AddAttachment(attachment, outAttachment);
        }
        else
        {
            _messageBuilder.SetText(telegramMessage.Text!);
        }

        ResetReply();
    }

    public Message Build() => _messageBuilder.Build();

    private static IReplyMarkup? CreateReplyMarkup(OutMessage message)
    {
        if (message.InlineButtons.Count > 0)
        {
            var rows = message.InlineButtons.Select(row =>
            {
                return row.Select(button => button switch
                {
                    CallbackInlineButton(var text, var data) => InlineKeyboardButton.WithCallbackData(text, data),
                    LinkInlineButton(var text, var url) => InlineKeyboardButton.WithUrl(text, url.ToString()),
                    _ => throw new TelegramAdapterException("Unsupported inline button type")
                });
            });

            return new InlineKeyboardMarkup(rows);
        }

        if (message.Keyboard.Count > 0)
        {
            if (message.Keyboard.RemoveKeyboard)
            {
                return new ReplyKeyboardRemove();
            }

            var rows = message.Keyboard.Select(row =>
            {
                return row.Select(button => new KeyboardButton(button.Text));
            });

            return new ReplyKeyboardMarkup(rows) { ResizeKeyboard = true };
        }

        return null;
    }
}
