using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Models;
using Replikit.Abstractions.Management.Services;

namespace Replikit.Adapters.Common.Services.Internal;

internal class CommonMemberService : AdapterService, IMemberService
{
    private readonly IMemberService _memberService;

    public MemberServiceFeatures Features => _memberService.Features;

    public CommonMemberService(AdapterIdentifier adapterId, IMemberService memberService) : base(adapterId)
    {
        _memberService = memberService;
    }

    public Task<IReadOnlyList<MemberInfo>> GetManyAsync(Identifier channelId,
        IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifiers(accountIds);

        return _memberService.GetManyAsync(channelId, accountIds, cancellationToken);
    }

    public Task<IReadOnlyList<MemberInfo>> ListManyAsync(Identifier channelId, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);

        return _memberService.ListManyAsync(channelId, take, skip, cancellationToken);
    }

    public Task AddAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(accountId);

        return _memberService.AddAsync(channelId, accountId, cancellationToken);
    }

    public Task RemoveAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(accountId);

        return _memberService.RemoveAsync(channelId, accountId, cancellationToken);
    }

    public Task BanAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(accountId);

        return _memberService.BanAsync(channelId, accountId, cancellationToken);
    }

    public Task UnbanAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(accountId);

        return _memberService.UnbanAsync(channelId, accountId, cancellationToken);
    }

    public Task<long> GetTotalCountAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);

        return _memberService.GetTotalCountAsync(channelId, cancellationToken);
    }
}
