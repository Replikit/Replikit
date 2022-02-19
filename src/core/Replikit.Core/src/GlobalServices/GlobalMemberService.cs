using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Features;
using Replikit.Abstractions.Management.Models;

namespace Replikit.Core.GlobalServices;

internal class GlobalMemberService : IGlobalMemberService
{
    private readonly IAdapterCollection _adapterCollection;

    public GlobalMemberService(IAdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public MemberServiceFeatures GetFeatures(AdapterIdentifier adapterId)
    {
        return _adapterCollection.ResolveRequired(adapterId).MemberService.Features;
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

    public Task<IReadOnlyList<MemberInfo>> ListManyAsync(GlobalIdentifier channelId, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).ListManyAsync(channelId, take, skip, cancellationToken);
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

    public Task BanAsync(GlobalIdentifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).BanAsync(channelId, accountId, cancellationToken);
    }

    public Task UnbanAsync(GlobalIdentifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).UnbanAsync(channelId, accountId, cancellationToken);
    }

    public Task<long> GetTotalCountAsync(GlobalIdentifier channelId, CancellationToken cancellationToken = default)
    {
        return ResolveMemberService(channelId).GetTotalCountAsync(channelId, cancellationToken);
    }
}
