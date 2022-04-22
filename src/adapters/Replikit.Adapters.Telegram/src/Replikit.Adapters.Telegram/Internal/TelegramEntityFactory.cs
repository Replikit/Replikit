using System.Globalization;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Adapters.Common.Exceptions;
using Replikit.Adapters.Common.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Message = Replikit.Abstractions.Messages.Models.Message;
using PhotoSize = Replikit.Abstractions.Attachments.Models.PhotoSize;
using TelegramMessage = Telegram.Bot.Types.Message;
using TelegramPhotoSize = Telegram.Bot.Types.PhotoSize;

namespace Replikit.Adapters.Telegram.Internal;

internal class TelegramEntityFactory : AdapterService
{
    public TelegramEntityFactory(AdapterIdentifier adapterId) : base(adapterId) { }

    public AccountInfo CreateAccountInfo(Chat chat, PhotoAttachment? avatar)
    {
        return new AccountInfo(
            CreateGlobalIdentifier(chat.Id),
            chat.Username,
            chat.FirstName,
            chat.LastName,
            avatar
        );
    }

    public AccountInfo CreateAccountInfo(User user, PhotoAttachment? avatar)
    {
        return new AccountInfo(
            CreateGlobalIdentifier(user.Id),
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

        var channelTitle = chat switch
        {
            { Title: { } title } => title,
            { FirstName: { } firstName, LastName: { } lastName } => $"{firstName} {lastName}",
            { FirstName: { } firstName } => firstName,
            { Username: { } username } => username,
            _ => null
        };

        return new ChannelInfo(CreateGlobalIdentifier(chat.Id), type, channelTitle);
    }

    public Attachment? ExtractAttachment(TelegramMessage message)
    {
        return message.Type switch
        {
            MessageType.Photo => CreatePhotoAttachment(message.Photo!, message.Caption),
            MessageType.Video => CreateVideoAttachment(message.Video!, message.Caption),
            MessageType.Document => CreateDocumentAttachment(message.Document!, message.Caption),
            MessageType.Voice => CreateVoiceAttachment(message.Voice!, message.Caption),
            MessageType.Audio => CreateAudioAttachment(message.Audio!, message.Caption),
            MessageType.Sticker => CreateStickerAttachment(message.Sticker!, message.Caption),
            _ => null
        };
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
            CreateGlobalIdentifier(firstMessage.From!.Id),
            firstMessage.Text ?? firstMessage.Caption,
            firstMessage.ReplyToMessage is not null ? CreateMessage(new[] { firstMessage.ReplyToMessage }) : null
        );
    }

    public PhotoAttachment CreatePhotoAttachment(IEnumerable<TelegramPhotoSize> photoSizes, string? caption = null)
    {
        var sizes = photoSizes
            .Select(x => new PhotoSize(
                CreateGlobalIdentifier(x.FileUniqueId),
                caption,
                Width: x.Width,
                Height: x.Width,
                Size: x.FileSize,
                UploadId: x.FileId
            ))
            .ToList();

        return new PhotoAttachment(sizes);
    }

    private StickerAttachment CreateStickerAttachment(Sticker messageSticker, string? caption)
    {
        return new StickerAttachment(
            CreateGlobalIdentifier(messageSticker.FileUniqueId),
            caption,
            Size: messageSticker.FileSize,
            UploadId: messageSticker.FileId
        );
    }

    private AudioAttachment CreateAudioAttachment(Audio messageAudio, string? caption)
    {
        return new AudioAttachment(
            CreateGlobalIdentifier(messageAudio.FileUniqueId),
            caption,
            Title: messageAudio.Title,
            Size: messageAudio.FileSize,
            UploadId: messageAudio.FileId,
            Duration: messageAudio.Duration
        );
    }

    private VoiceAttachment CreateVoiceAttachment(Voice messageVoice, string? caption)
    {
        return new VoiceAttachment(
            CreateGlobalIdentifier(messageVoice.FileUniqueId),
            caption,
            Size: messageVoice.FileSize,
            UploadId: messageVoice.FileId,
            Duration: messageVoice.Duration
        );
    }

    private DocumentAttachment CreateDocumentAttachment(Document messageDocument, string? caption)
    {
        return new DocumentAttachment(
            CreateGlobalIdentifier(messageDocument.FileUniqueId),
            caption,
            FileName: messageDocument.FileName,
            Size: messageDocument.FileSize,
            UploadId: messageDocument.FileId
        );
    }

    private VideoAttachment CreateVideoAttachment(Video video, string? caption)
    {
        return new VideoAttachment(
            CreateGlobalIdentifier(video.FileUniqueId),
            caption,
            Size: video.FileSize,
            Duration: video.Duration,
            UploadId: video.FileId
        );
    }
}
