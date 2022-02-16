using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Features;
using Telegram.Bot;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramMemberService : IMemberService
{
    private readonly ITelegramBotClient _backend;

    public TelegramMemberService(ITelegramBotClient backend)
    {
        _backend = backend;
    }

    public MemberCollectionFeatures Features =>
        MemberCollectionFeatures.GetTotalCount |
        MemberCollectionFeatures.Remove;

    public async Task<long> GetTotalCountAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        return await _backend.GetChatMembersCountAsync((long) channelId, cancellationToken);
    }

    public Task RemoveAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
    {
        return _backend.KickChatMemberAsync((long) channelId, accountId, cancellationToken: cancellationToken);
    }
}
