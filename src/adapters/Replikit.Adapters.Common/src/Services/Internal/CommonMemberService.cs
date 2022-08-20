using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Abstractions.Members.Models;
using Replikit.Abstractions.Members.Services;

namespace Replikit.Adapters.Common.Services.Internal;

internal class CommonMemberService : AdapterService, IMemberService
{
    private readonly IMemberService _memberService;

    public CommonMemberService(IAdapter adapter, IMemberService memberService) : base(adapter)
    {
        _memberService = memberService;
    }

    public MemberServiceFeatures Features => _memberService.Features;

    public Task<MemberInfo?> GetAsync(Identifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(accountId);

        // TODO caching
        return _memberService.GetAsync(channelId, accountId, cancellationToken);
    }

    public Task<IReadOnlyList<MemberInfo>> GetManyAsync(Identifier channelId,
        IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotNull(accountIds);

        return _memberService.GetManyAsync(channelId, accountIds, cancellationToken);
    }

    public Task<MemberInfo?> AddAsync(Identifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(accountId);

        return _memberService.AddAsync(channelId, accountId, cancellationToken);
    }

    public Task RemoveAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(accountId);

        return _memberService.RemoveAsync(channelId, accountId, cancellationToken);
    }

    public Task<long> GetTotalCountAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);

        return _memberService.GetTotalCountAsync(channelId, cancellationToken);
    }
}
