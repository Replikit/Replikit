using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Accounts.Services;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Adapters.Telegram.Internal;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramAccountService : IAccountService
{
    private readonly ITelegramBotClient _backend;
    private readonly TelegramEntityFactory _entityFactory;

    public TelegramAccountService(ITelegramBotClient backend, TelegramEntityFactory entityFactory)
    {
        _backend = backend;
        _entityFactory = entityFactory;
    }

    public AccountServiceFeatures Features =>
        AccountServiceFeatures.Get |
        AccountServiceFeatures.GetAvatar;

    public async Task<AccountInfo?> GetAsync(Identifier accountId, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _backend.GetChatAsync((long) accountId, cancellationToken);

            return _entityFactory.CreateAccountInfo(user);
        }
        catch (ApiRequestException e) when (e.Message.Contains("chat not found"))
        {
            return null;
        }
    }

    public async Task<PhotoAttachment?> GetAvatarAsync(Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        var photos = await _backend.GetUserProfilePhotosAsync(accountId, cancellationToken: cancellationToken);

        return photos.TotalCount > 0 ? _entityFactory.CreatePhotoAttachment(photos.Photos[0]) : null;
    }
}
