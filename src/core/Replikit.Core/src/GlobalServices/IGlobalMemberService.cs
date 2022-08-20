using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Members.Models;
using Replikit.Abstractions.Members.Services;

namespace Replikit.Core.GlobalServices;

public interface IGlobalMemberService : IGlobalHasFeatures<MemberServiceFeatures>
{
    /// <inheritdoc cref="IMemberService.GetManyAsync" />
    Task<IReadOnlyList<MemberInfo>> GetManyAsync(GlobalIdentifier channelId, IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.AddAsync" />
    Task AddAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.RemoveAsync" />
    Task RemoveAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.GetTotalCountAsync" />
    Task<long> GetTotalCountAsync(GlobalIdentifier channelId, CancellationToken cancellationToken = default);
}
