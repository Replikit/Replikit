using System.Collections.Immutable;
using System.Globalization;
using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
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
    public TelegramEntityFactory(IAdapter adapter) : base(adapter) { }

    public AccountInfo CreateAccountInfo(Chat chat)
    {
        return new AccountInfo(CreateGlobalIdentifier(chat.Id))
        {
            Username = chat.Username,
            FirstName = chat.FirstName,
            LastName = chat.LastName,
            CustomData = ImmutableArray.Create(chat)
        };
    }

    public AccountInfo CreateAccountInfo(User user)
    {
        return new AccountInfo(CreateGlobalIdentifier(user.Id))
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CultureInfo = GetCultureInfo(user.LanguageCode),
            CustomData = ImmutableArray.Create(user)
        };
    }

    public AccountInfo CreateAccountInfo(User user, BotIdentifier botId)
    {
        return new AccountInfo(new GlobalIdentifier(botId, user.Id))
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CultureInfo = GetCultureInfo(user.LanguageCode),
            CustomData = ImmutableArray.Create(user)
        };
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

        return new ChannelInfo(CreateGlobalIdentifier(chat.Id), type)
        {
            Title = channelTitle,
            CustomData = ImmutableArray.Create(chat)
        };
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

        return new Message(identifier)
        {
            Attachments = attachments,
            CustomData = messages.ToImmutableArray(),
            ChannelId = CreateGlobalIdentifier(firstMessage.Chat.Id),
            AccountId = CreateGlobalIdentifier(firstMessage.From!.Id),
            Text = firstMessage.Text ?? firstMessage.Caption,
            ReplyId = firstMessage.ReplyToMessage?.MessageId,
            Reply = firstMessage.ReplyToMessage is not null
                ? CreateMessage(new[] { firstMessage.ReplyToMessage })
                : null,
        };
    }

    public PhotoAttachment CreatePhotoAttachment(IEnumerable<TelegramPhotoSize> photoSizes, string? caption = null)
    {
        var sizes = photoSizes
            .Select(CreatePhotoSize)
            .ToImmutableArray();

        return new PhotoAttachment(sizes[0].Id, sizes) { Caption = caption };
    }

    private PhotoSize CreatePhotoSize(TelegramPhotoSize photoSize)
    {
        return new PhotoSize(CreateGlobalIdentifier(photoSize.FileUniqueId))
        {
            Width = photoSize.Width,
            Height = photoSize.Height,
            FileSize = photoSize.FileSize,
            UploadId = photoSize.FileId,
            CustomData = ImmutableArray.Create(photoSize)
        };
    }

    private StickerAttachment CreateStickerAttachment(Sticker messageSticker, string? caption)
    {
        return new StickerAttachment(CreateGlobalIdentifier(messageSticker.FileUniqueId))
        {
            Caption = caption,
            FileSize = messageSticker.FileSize,
            UploadId = messageSticker.FileId,
            CustomData = ImmutableArray.Create(messageSticker)
        };
    }

    private AudioAttachment CreateAudioAttachment(Audio messageAudio, string? caption)
    {
        return new AudioAttachment(CreateGlobalIdentifier(messageAudio.FileUniqueId))
        {
            Caption = caption,
            FileSize = messageAudio.FileSize,
            UploadId = messageAudio.FileId,
            Duration = messageAudio.Duration,
            CustomData = ImmutableArray.Create(messageAudio)
        };
    }

    private VoiceAttachment CreateVoiceAttachment(Voice messageVoice, string? caption)
    {
        return new VoiceAttachment(CreateGlobalIdentifier(messageVoice.FileUniqueId))
        {
            Caption = caption,
            FileSize = messageVoice.FileSize,
            UploadId = messageVoice.FileId,
            Duration = messageVoice.Duration,
            CustomData = ImmutableArray.Create(messageVoice)
        };
    }

    private DocumentAttachment CreateDocumentAttachment(Document messageDocument, string? caption)
    {
        return new DocumentAttachment(CreateGlobalIdentifier(messageDocument.FileUniqueId))
        {
            Caption = caption,
            FileSize = messageDocument.FileSize,
            FileName = messageDocument.FileName,
            UploadId = messageDocument.FileId,
            CustomData = ImmutableArray.Create(messageDocument)
        };
    }

    private VideoAttachment CreateVideoAttachment(Video video, string? caption)
    {
        return new VideoAttachment(CreateGlobalIdentifier(video.FileUniqueId))
        {
            Caption = caption,
            FileSize = video.FileSize,
            UploadId = video.FileId,
            Duration = video.Duration,
            CustomData = ImmutableArray.Create(video)
        };
    }

    private static CultureInfo? GetCultureInfo(string? languageCode)
    {
        return languageCode is null ? null : CultureInfo.GetCultureInfo(languageCode);
    }
}
