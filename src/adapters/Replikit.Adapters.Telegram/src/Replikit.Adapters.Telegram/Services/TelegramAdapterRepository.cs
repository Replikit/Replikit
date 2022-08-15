using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Abstractions.Repositories.Services;
using Replikit.Adapters.Common.Extensions;
using Replikit.Adapters.Common.Services;
using Replikit.Adapters.Telegram.Internal;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramAdapterRepository : AdapterRepository
{
    private readonly ITelegramBotClient _backend;
    private readonly string _token;
    private readonly TelegramEntityFactory _entityFactory;

    private readonly IMemoryCache _attachmentUrlCache = new MemoryCache(new MemoryCacheOptions());
    private readonly IMemoryCache _avatarCache = new MemoryCache(new MemoryCacheOptions());

    private readonly TimeSpan _attachmentUrlCacheLifetime = TimeSpan.FromMinutes(60);
    private readonly TimeSpan _avatarCacheLifetime = TimeSpan.FromMinutes(60);

    public override AdapterRepositoryFeatures Features =>
        AdapterRepositoryFeatures.GetAccountInfo |
        AdapterRepositoryFeatures.GetChannelInfo |
        AdapterRepositoryFeatures.ResolveAttachmentUrl;

    public TelegramAdapterRepository(TelegramAdapterOptions options, ITelegramBotClient backend,
        TelegramEntityFactory entityFactory)
    {
        _token = options.Token;
        _backend = backend;
        _entityFactory = entityFactory;
    }

    protected override async Task<ChannelInfo?> FetchChannelInfo(Identifier identifier,
        CancellationToken cancellationToken)
    {
        try
        {
            var chat = await _backend.GetChatAsync((long) identifier, cancellationToken);

            return _entityFactory.CreateChannelInfo(chat);
        }
        catch (ApiRequestException e) when (e.Message.Contains("chat not found"))
        {
            return null;
        }
    }

    protected override async Task<AccountInfo?> FetchAccountInfo(Identifier identifier,
        CancellationToken cancellationToken)
    {
        try
        {
            var avatar = await GetUserAvatarAsync(identifier, cancellationToken);
            var user = await _backend.GetChatAsync((long) identifier, cancellationToken);

            return _entityFactory.CreateAccountInfo(user, avatar);
        }
        catch (ApiRequestException e) when (e.Message.Contains("chat not found"))
        {
            return null;
        }
    }

    private string CreateFileUrl(string path)
    {
        return $"https://api.telegram.org/file/bot{_token}/{path}";
    }

    private async Task<string?> FetchFileUrl(GlobalIdentifier identifier, CancellationToken cancellationToken)
    {
        try
        {
            var file = await _backend.GetFileAsync(identifier.Value, cancellationToken);

            Debug.Assert(file.FilePath is not null);
            return CreateFileUrl(file.FilePath);
        }
        catch (ApiRequestException e) when (e.Message.Contains("file is temporarily unavailable"))
        {
            return null;
        }
    }

    private async Task<PhotoAttachment?> FetchUserAvatar(Identifier identifier, CancellationToken cancellationToken)
    {
        var photoSizes = await _backend.GetUserProfilePhotosAsync(identifier, cancellationToken: cancellationToken);
        return photoSizes.TotalCount > 0 ? _entityFactory.CreatePhotoAttachment(photoSizes.Photos[0]) : null;
    }

    public ChannelInfo UpdateChannelInfo(Chat chat)
    {
        var channelInfo = _entityFactory.CreateChannelInfo(chat);
        return UpdateChannelInfo(channelInfo);
    }

    public async Task<AccountInfo> UpdateAccountInfo(Chat chat, CancellationToken cancellationToken = default)
    {
        var avatar = await GetUserAvatarAsync(chat.Id, cancellationToken);
        var accountInfo = _entityFactory.CreateAccountInfo(chat, avatar);

        return UpdateAccountInfo(accountInfo);
    }

    public async Task<AccountInfo> UpdateAccountInfo(User user, CancellationToken cancellationToken = default)
    {
        var avatar = await GetUserAvatarAsync(user.Id, cancellationToken);
        var accountInfo = _entityFactory.CreateAccountInfo(user, avatar);

        return UpdateAccountInfo(accountInfo);
    }

    public override async Task<Attachment> ResolveAttachmentUrlAsync(Attachment attachment,
        CancellationToken cancellationToken = default)
    {
        if (attachment.Url is not null) return attachment;

        var url = await _attachmentUrlCache.GetOrCreateAsync(attachment.Id, FetchFileUrl,
            _attachmentUrlCacheLifetime, cancellationToken);

        return attachment with { Url = url };
    }

    private Task<PhotoAttachment?> GetUserAvatarAsync(Identifier identifier, CancellationToken cancellationToken)
    {
        return _avatarCache.GetOrCreateAsync(identifier, FetchUserAvatar, _avatarCacheLifetime, cancellationToken);
    }
}
