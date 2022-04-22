using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;
using Replikit.Abstractions.Messages.Services;
using Replikit.Adapters.Common.Models;
using Replikit.Adapters.Telegram.Exceptions;
using Replikit.Adapters.Telegram.Internal;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Message = Replikit.Abstractions.Messages.Models.Message;
using TelegramMessage = Telegram.Bot.Types.Message;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramMessageService : IMessageService
{
    private readonly ITelegramBotClient _backend;
    private readonly TelegramMessageResolver _telegramMessageResolver;
    private readonly TelegramEntityFactory _telegramEntityFactory;

    public TelegramMessageService(ITelegramBotClient backend, TelegramMessageResolver telegramMessageResolver,
        TelegramEntityFactory telegramEntityFactory)
    {
        _backend = backend;
        _telegramMessageResolver = telegramMessageResolver;
        _telegramEntityFactory = telegramEntityFactory;
    }

    public MessageServiceFeatures Features =>
        MessageServiceFeatures.Send |
        MessageServiceFeatures.Edit |
        MessageServiceFeatures.Delete |
        MessageServiceFeatures.Pin |
        MessageServiceFeatures.Unpin |
        MessageServiceFeatures.AnswerInlineButtonRequest;

    private const int MaxAttachmentCount = 10;

    public async Task<Message> SendAsync(Identifier channelId, OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var chatId = new ChatId((long) channelId);

        var (text, attachments) = await _telegramMessageResolver.ResolveMessageAsync(message, cancellationToken);

        var messageBuilder = new TelegramMessageBuilder(_telegramEntityFactory, message);

        if (!string.IsNullOrEmpty(text))
        {
            var result = await _backend.SendTextMessageAsync(chatId,
                text, ParseMode.Html,
                replyToMessageId: messageBuilder.ReplyToMessageId,
                replyMarkup: messageBuilder.ReplyMarkup,
                cancellationToken: cancellationToken);

            messageBuilder.ApplyResult(result);
        }

        var mediaAttachments = new List<IAlbumInputMedia>(MaxAttachmentCount);

        async Task SendMediaAttachments()
        {
            var results = await _backend.SendMediaGroupAsync(chatId, mediaAttachments,
                replyToMessageId: messageBuilder.ReplyToMessageId,
                cancellationToken: cancellationToken);

            foreach (var result in results)
            {
                messageBuilder.ApplyResult(result);
            }
        }

        foreach (var (original, source) in attachments)
        {
            IAlbumInputMedia? inputMedia = original.Type switch
            {
                AttachmentType.Photo => new InputMediaPhoto(source) { Caption = original.Caption },
                AttachmentType.Video => new InputMediaVideo(source) { Caption = original.Caption },
                _ => null
            };

            if (inputMedia is not null)
            {
                mediaAttachments.Add(inputMedia);
            }

            if (mediaAttachments.Count == MaxAttachmentCount)
            {
                await SendMediaAttachments();
                mediaAttachments.Clear();
            }
        }

        if (mediaAttachments.Count > 0)
        {
            await SendMediaAttachments();
        }

        foreach (var (attachment, source) in attachments)
        {
            var result = attachment.Type switch
            {
                AttachmentType.Document => await _backend.SendDocumentAsync(chatId, source,
                    caption: attachment.Caption,
                    replyToMessageId: messageBuilder.ReplyToMessageId,
                    replyMarkup: messageBuilder.ReplyMarkup,
                    cancellationToken: cancellationToken),
                AttachmentType.Audio => await _backend.SendAudioAsync(chatId, source,
                    attachment.Caption,
                    replyToMessageId: messageBuilder.ReplyToMessageId,
                    replyMarkup: messageBuilder.ReplyMarkup,
                    cancellationToken: cancellationToken),
                AttachmentType.Voice => await _backend.SendVoiceAsync(chatId, source,
                    attachment.Caption,
                    replyToMessageId: messageBuilder.ReplyToMessageId,
                    replyMarkup: messageBuilder.ReplyMarkup,
                    cancellationToken: cancellationToken),
                AttachmentType.Sticker => await _backend.SendStickerAsync(chatId, source,
                    replyToMessageId: messageBuilder.ReplyToMessageId,
                    replyMarkup: messageBuilder.ReplyMarkup,
                    cancellationToken: cancellationToken),
                _ => null
            };

            if (result is not null)
            {
                messageBuilder.ApplyResult(result);
            }
        }

        foreach (var forwardedMessage in message.ForwardedMessages)
        {
            foreach (var messageIdentifier in forwardedMessage.Identifier.Identifiers)
            {
                var result = await _backend.ForwardMessageAsync(chatId, (long) forwardedMessage.ChannelId,
                    messageIdentifier,
                    cancellationToken: cancellationToken);

                messageBuilder.ApplyResult(result);
            }
        }

        return messageBuilder.Build();
    }

    public Task<Message> EditAsync(Identifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage = null, CancellationToken cancellationToken = default)
    {
        var chatId = new ChatId((long) channelId);

        if (messageId.Identifiers.Count == 1)
        {
            return EditSingleMessageAsync(chatId, messageId.Identifiers[0], message, cancellationToken);
        }

        if (oldMessage is null)
        {
            throw new TelegramAdapterException("Telegram adapter cannot edit complex message without oldMessage");
        }

        return EditComplexMessageAsync(chatId, messageId, message, oldMessage, cancellationToken);
    }

    private Task<Message> EditComplexMessageAsync(ChatId chatId, MessageIdentifier messageId, OutMessage message,
        OutMessage oldMessage, CancellationToken cancellationToken)
    {
        throw new TelegramAdapterException("Editing complex messages is currently unsupported");
    }

    private async Task<Message> EditSingleMessageAsync(ChatId chatId, Identifier messageId,
        OutMessage message, CancellationToken cancellationToken)
    {
        var (text, attachments) = await _telegramMessageResolver.ResolveMessageAsync(message, cancellationToken);

        var messageBuilder = new TelegramMessageBuilder(_telegramEntityFactory, message);

        if (!string.IsNullOrEmpty(text) && attachments.Count > 0 || attachments.Count > 1)
        {
            throw new TelegramAdapterException(
                "OutMessage was expected to be single since singular identifier was provided");
        }

        if (!string.IsNullOrEmpty(text))
        {
            var result = await _backend.EditMessageTextAsync(chatId, messageId,
                text, ParseMode.Html,
                replyMarkup: messageBuilder.ReplyMarkup as InlineKeyboardMarkup,
                cancellationToken: cancellationToken);

            messageBuilder.ApplyResult(result);
            return messageBuilder.Build();
        }

        if (attachments.Count == 1)
        {
            var result = await EditAttachmentMessage(chatId, messageId, attachments[0], cancellationToken);

            if (result is not null)
            {
                messageBuilder.ApplyResult(result);
            }

            return messageBuilder.Build();
        }

        throw new TelegramAdapterException("Empty message");
    }

    private async Task<TelegramMessage?> EditAttachmentMessage(ChatId chatId, Identifier messageId,
        ResolvedAttachment<InputMedia> attachment,
        CancellationToken cancellationToken)
    {
        var (original, source) = attachment;

        InputMediaBase? inputMedia = original.Type switch
        {
            AttachmentType.Photo => new InputMediaPhoto(source) { Caption = original.Caption },
            AttachmentType.Video => new InputMediaVideo(source) { Caption = original.Caption },
            AttachmentType.Audio => new InputMediaAudio(source) { Caption = original.Caption },
            AttachmentType.Document => new InputMediaDocument(source) { Caption = original.Caption },
            _ => null
        };

        if (inputMedia is null) return null;

        return await _backend.EditMessageMediaAsync(chatId,
            messageId, inputMedia,
            cancellationToken: cancellationToken);
    }

    public Task DeleteAsync(Identifier channelId, Identifier messageId, CancellationToken cancellationToken = default)
    {
        return _backend.DeleteMessageAsync((long) channelId, messageId, cancellationToken);
    }

    public Task PinAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        return _backend.PinChatMessageAsync((long) channelId, messageId.Identifiers[0],
            cancellationToken: cancellationToken);
    }

    public Task UnpinAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        return _backend.UnpinChatMessageAsync((long) channelId, messageId.Identifiers[0], cancellationToken);
    }

    public Task AnswerInlineButtonRequestAsync(Identifier requestId, string message,
        CancellationToken cancellationToken = default)
    {
        return _backend.AnswerCallbackQueryAsync(requestId, message, cancellationToken: cancellationToken);
    }
}
