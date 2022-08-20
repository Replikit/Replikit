using Replikit.Abstractions.Channels.Models;
using Replikit.Abstractions.Channels.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Adapters.Telegram.Internal;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramChannelService : IChannelService
{
    private readonly ITelegramBotClient _backend;
    private readonly TelegramEntityFactory _entityFactory;

    public TelegramChannelService(ITelegramBotClient backend, TelegramEntityFactory entityFactory)
    {
        _backend = backend;
        _entityFactory = entityFactory;
    }

    public ChannelServiceFeatures Features =>
        ChannelServiceFeatures.Get |
        ChannelServiceFeatures.ChangeTitle;

    public async Task<ChannelInfo?> GetAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        try
        {
            var chat = await _backend.GetChatAsync((long) channelId, cancellationToken);

            return _entityFactory.CreateChannelInfo(chat);
        }
        catch (ApiRequestException e) when (e.Message.Contains("chat not found"))
        {
            return null;
        }
    }

    public Task ChangeTitleAsync(Identifier channelId, string title, CancellationToken cancellationToken = default)
    {
        return _backend.SetChatTitleAsync((long) channelId, title, cancellationToken);
    }
}
