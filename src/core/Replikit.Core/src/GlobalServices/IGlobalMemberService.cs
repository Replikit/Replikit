using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Features;
using Replikit.Abstractions.Management.Models;

namespace Replikit.Core.GlobalServices;

public interface IGlobalMemberService : IGlobalHasFeatures<MemberServiceFeatures>
{
    /// <summary>
    /// Finds multiple members with specified identifiers.
    /// Can return less members that ids specified, if some members could not be found.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<MemberInfo>> GetManyAsync(GlobalIdentifier channelId, IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// List multiple members.
    /// Returns as many members as could list according to specified take and skip options.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<MemberInfo>> ListManyAsync(GlobalIdentifier channelId, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds user with specified identifier.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes user with specified identifier.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds user with specified identifier to ban-list.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BanAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes user with specified identifier from ban-list.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UnbanAsync(GlobalIdentifier channelId, Identifier accountId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the total count of members in the specified channel.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<long> GetTotalCountAsync(GlobalIdentifier channelId, CancellationToken cancellationToken = default);
}
