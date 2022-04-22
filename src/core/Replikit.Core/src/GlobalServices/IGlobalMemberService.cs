using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Models;
using Replikit.Abstractions.Management.Services;

namespace Replikit.Core.GlobalServices;

public interface IGlobalMemberService : IGlobalHasFeatures<MemberServiceFeatures>
{
    /// <inheritdoc cref="IMemberService.GetManyAsync" />
    Task<IReadOnlyList<MemberInfo>> GetManyAsync(GlobalIdentifier channelId, IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.ListManyAsync" />
    Task<IReadOnlyList<MemberInfo>> ListManyAsync(GlobalIdentifier channelId, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.AddAsync" />
    Task AddAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.RemoveAsync" />
    Task RemoveAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.BanAsync" />
    Task BanAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.UnbanAsync" />
    Task UnbanAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMemberService.GetTotalCountAsync" />
    Task<long> GetTotalCountAsync(GlobalIdentifier channelId, CancellationToken cancellationToken = default);
}
