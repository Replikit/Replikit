using System.Globalization;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Adapters.Common.Exceptions;
using Replikit.Adapters.Common.Features;
using Replikit.Adapters.Telegram.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Message = Replikit.Abstractions.Messages.Models.Message;
using PhotoSize = Replikit.Abstractions.Attachments.Models.PhotoSize;
using TelegramMessage = Telegram.Bot.Types.Message;
using TelegramPhotoSize = Telegram.Bot.Types.PhotoSize;

namespace Replikit.Adapters.Telegram.Internal;

internal class TelegramEntityFactory : AdapterService
{
    public TelegramEntityFactory(IAdapter adapter) : base(adapter) { }

    public PhotoAttachment CreatePhotoAttachment(IEnumerable<TelegramPhotoSize> photoSizes, string? caption = null)
    {
        var sizes = photoSizes
            .Select(x => new PhotoSize(CreateGlobalIdentifier(x.FileUniqueId), caption,
                Width: x.Width,
                Height: x.Width,
                Size: x.FileSize))
            .ToList();

        return new PhotoAttachment(sizes);
    }

    public VideoAttachment CreateVideoAttachment(Video video, string? caption)
    {
        return new VideoAttachment(CreateGlobalIdentifier(video.FileUniqueId), caption,
            Size: video.FileSize,
            Duration: video.Duration);
    }

    public AccountInfo CreateAccountInfo(Chat chat, PhotoAttachment? avatar)
    {
        return new AccountInfo(CreateGlobalIdentifier(chat.Id), chat.Username, chat.FirstName, chat.LastName, avatar);
    }

    public AccountInfo CreateAccountInfo(User user, PhotoAttachment? avatar)
    {
        return new AccountInfo(CreateGlobalIdentifier(user.Id),
            user.Username,
            user.FirstName,
            user.LastName,
            avatar,
            user.LanguageCode is not null ? CultureInfo.GetCultureInfo(user.LanguageCode) : null);
    }

    public ChannelInfo CreateChannelInfo(Chat chat)
    {
        var type = chat.Type switch
        {
            ChatType.Channel => ChannelType.PostChannel,
            ChatType.Group => ChannelType.Group,
            ChatType.Supergroup => ChannelType.Group,
            ChatType.Private => ChannelType.Direct,
            _ => throw new ConversionException($"Unable to convert value {chat.Type} to ChannelType")
        };

        return new ChannelInfo(CreateGlobalIdentifier(chat.Id), type, chat.Title);
    }

    public Attachment? ExtractAttachment(TelegramMessage message)
    {
        switch (message.Type)
        {
            case MessageType.Photo:
                return CreatePhotoAttachment(message.Photo, message.Caption);
            case MessageType.Video:
                return CreateVideoAttachment(message.Video, message.Caption);
            case MessageType.Document:
                return CreateDocumentAttachment(message.Document, message.Caption);
            case MessageType.Voice:
                return CreateVoiceAttachment(message.Voice, message.Caption);
            case MessageType.Audio:
                return CreateAudioAttachment(message.Audio, message.Caption);
            case MessageType.Sticker:
                return CreateStickerAttachment(message.Sticker, message.Caption);
        }

        return null;
    }

    public Message CreateMessage(IReadOnlyList<TelegramMessage> messages)
    {
        var ids = new List<Identifier>();
        var attachments = new List<Attachment>();

        foreach (var message in messages)
        {
            ids.Add(message.MessageId);
            var attachment = ExtractAttachment(message);
            if (attachment is not null) attachments.Add(attachment);
        }

        var firstMessage = messages[0];
        var identifier = new GlobalMessageIdentifier(CreateGlobalIdentifier(firstMessage.Chat.Id), ids);

        return new Message(identifier,
            attachments,
            messages,
            CreateGlobalIdentifier(firstMessage.Chat.Id),
            CreateGlobalIdentifier(firstMessage.From.Id),
            firstMessage.Text ?? firstMessage.Caption,
            firstMessage.ReplyToMessage is not null ? CreateMessage(new[] { firstMessage.ReplyToMessage }) : null);
    }

    private StickerAttachment CreateStickerAttachment(Sticker messageSticker, string? caption)
    {
        return new StickerAttachment(CreateAttachmentId(messageSticker), caption, Size: messageSticker.FileSize);
    }

    private AudioAttachment CreateAudioAttachment(Audio messageAudio, string? caption)
    {
        return new AudioAttachment(CreateAttachmentId(messageAudio), caption,
            Title: messageAudio.Title,
            Size: messageAudio.FileSize,
            Duration: messageAudio.Duration);
    }

    private VoiceAttachment CreateVoiceAttachment(Voice messageVoice, string? caption)
    {
        return new VoiceAttachment(CreateAttachmentId(messageVoice), caption,
            Size: messageVoice.FileSize,
            Duration: messageVoice.Duration);
    }

    private TelegramAttachmentIdentifier CreateAttachmentId(FileBase fileBase)
    {
        return new TelegramAttachmentIdentifier(CreateGlobalIdentifier(fileBase.FileUniqueId), fileBase.FileId);
    }

    public DocumentAttachment CreateDocumentAttachment(Document messageDocument, string? caption)
    {
        return new DocumentAttachment(CreateAttachmentId(messageDocument), caption,
            FileName: messageDocument.FileName,
            Size: messageDocument.FileSize);
    }
}
