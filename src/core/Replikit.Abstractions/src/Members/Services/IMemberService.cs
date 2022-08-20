using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Members.Models;

namespace Replikit.Abstractions.Members.Services;

/// <summary>
/// The service that provides methods to work with channel members.
/// </summary>
public interface IMemberService : IHasFeatures<MemberServiceFeatures>
{
    /// <summary>
    /// Gets the member of the channel with the specified id.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="accountId">An identifier of the account.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the member info or <c>null</c> if the member is not found.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<MemberInfo?> GetAsync(Identifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.Get);
    }

    /// <summary>
    /// Gets the members with the specified account identifiers.
    /// Can return less members that ids specified, if some members could not be found.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="accountIds">A collection of account identifiers.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a list of <see cref="MemberInfo"/> objects.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<IReadOnlyList<MemberInfo>> GetManyAsync(Identifier channelId, IReadOnlyCollection<Identifier> accountIds,
        CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.GetMany);
    }

    /// <summary>
    /// Adds the user to the channel.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="accountId">An identifier of the account.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the member info or <c>null</c> if the member was not added.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<MemberInfo?> AddAsync(Identifier channelId, Identifier accountId,
        CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.Add);
    }

    /// <summary>
    /// Removes the user from the channel.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="accountId">An identifier of the account.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task RemoveAsync(Identifier channelId, Identifier accountId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.Remove);
    }

    /// <summary>
    /// Gets the total count of members in the channel.
    /// </summary>
    /// <param name="channelId">An identifier of the channel.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the total count of members in the channel.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<long> GetTotalCountAsync(Identifier channelId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MemberServiceFeatures.GetTotalCount);
    }
}
