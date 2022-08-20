using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Accounts.Services;

/// <summary>
/// The service that provides methods to work with accounts.
/// </summary>
public interface IAccountService : IHasFeatures<AccountServiceFeatures>
{
    /// <summary>
    /// Gets the account with the specified identifier.
    /// </summary>
    /// <param name="accountId">An identifier of the account.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the account or <c>null</c> if it was not found.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<AccountInfo?> GetAsync(Identifier accountId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, AccountServiceFeatures.Get);
    }

    /// <summary>
    /// Gets the avatar of the account with the specified identifier.
    /// </summary>
    /// <param name="accountId">An identifier of the account.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the avatar or <c>null</c> if account was not found or has no avatar
    /// or it is not accessible to the bot.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<PhotoAttachment?> GetAvatarAsync(Identifier accountId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, AccountServiceFeatures.GetAvatar);
    }
}
