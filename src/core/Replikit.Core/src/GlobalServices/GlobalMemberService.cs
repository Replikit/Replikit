using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Members.Models;
using Replikit.Abstractions.Members.Services;

namespace Replikit.Core.GlobalServices;

internal class GlobalMemberService : IGlobalMemberService
{
    private readonly IAdapterCollection _adapterCollection;

    public GlobalMemberService(IAdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public MemberServiceFeatures GetFeatures(BotIdentifier botId)
    {
        return _adapterCollection.ResolveRequired(botId).MemberService.Features;
    }

    private IMemberService ResolveMemberService(GlobalIdentifier channelId)
    {
        return _adapterCollection.ResolveRequired(channelId).MemberService;
    }

    public Task<IReadOnlyList<MemberInfo>> GetManyAsync(GlobalIdentifier channelId,
        IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).GetManyAsync(channelId, accountIds, cancellationToken);
    }

    public Task AddAsync(GlobalIdentifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).AddAsync(channelId, accountId, cancellationToken);
    }

    public Task RemoveAsync(GlobalIdentifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).RemoveAsync(channelId, accountId, cancellationToken);
    }

    public Task<long> GetTotalCountAsync(GlobalIdentifier channelId, CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).GetTotalCountAsync(channelId, cancellationToken);
    }
}
