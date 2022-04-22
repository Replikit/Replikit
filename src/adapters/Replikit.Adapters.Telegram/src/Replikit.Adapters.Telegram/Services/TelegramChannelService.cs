using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Services;
using Replikit.Adapters.Common.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Replikit.Adapters.Telegram.Services;

public class TelegramChannelService : IChannelService
{
    private readonly ITelegramBotClient _backend;
    private readonly MessageResolver<InputMedia> _messageResolver;

    public TelegramChannelService(ITelegramBotClient backend, MessageResolver<InputMedia> messageResolver)
    {
        _backend = backend;
        _messageResolver = messageResolver;
    }

    public ChannelServiceFeatures Features =>
        ChannelServiceFeatures.ChangeTitle |
        ChannelServiceFeatures.ChangePhoto;

    public Task ChangeTitleAsync(Identifier channelId, string title, CancellationToken cancellationToken = default)
    {
        return _backend.SetChatTitleAsync((long) channelId, title, cancellationToken);
    }

    public async Task ChangePhotoAsync(Identifier channelId, OutAttachment photo,
        CancellationToken cancellationToken = default)
    {
        var attachment = await _messageResolver.ResolveAttachmentAsync(photo, cancellationToken);

        await _backend.SetChatPhotoAsync((long) channelId, attachment.Source, cancellationToken);
    }
}
