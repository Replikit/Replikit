using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Models;

namespace Replikit.Abstractions.Management.Features;

/// <summary>
/// Provides methods to work with members.
/// </summary>
public interface IMemberService : IHasFeatures<MemberServiceFeatures>
{
    /// <summary>
    /// Finds multiple members with specified identifiers.
    /// Can return less members that ids specified, if some members could not be found.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<MemberInfo>> GetManyAsync(Identifier channelId, IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.GetMany);

    /// <summary>
    /// List multiple members.
    /// Returns as many members as could list according to specified take and skip options.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<MemberInfo>> ListManyAsync(Identifier channelId, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.ListMany);

    /// <summary>
    /// Adds user with specified identifier.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.Add);

    /// <summary>
    /// Removes user with specified identifier.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
        => throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.Remove);

    /// <summary>
    /// Adds user with specified identifier to ban-list.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BanAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.Add);

    /// <summary>
    /// Removes user with specified identifier from ban-list.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="accountId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UnbanAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.Add);

    /// <summary>
    /// Gets the total count of members in the specified channel.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<long> GetTotalCountAsync(Identifier channelId, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.GetTotalCount);
}
