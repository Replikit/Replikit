using System.Collections.Immutable;
using System.Globalization;
using Replikit.Abstractions.Accounts.Services;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Accounts.Models;

/// <summary>
/// Represents an account on some platform (messenger or social network).
/// </summary>
public sealed class AccountInfo : IHasCustomData
{
    /// <summary>
    /// Creates a new instance of <see cref="AccountInfo"/>.
    /// </summary>
    /// <param name="id">An identifier of the account.</param>
    public AccountInfo(GlobalIdentifier id)
    {
        Id = Check.NotDefault(id);
    }

    /// <summary>
    /// The global identifier of the account.
    /// </summary>
    public GlobalIdentifier Id { get; }

    /// <summary>
    /// The username of the account.
    /// <br/>
    /// May be null if the account doesn't have a username or it is not available to the bot.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// The first name of the account.
    /// <br/>
    /// May be null if the account doesn't have a first name or it is not available to the bot.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// The last name of the account.
    /// <br/>
    /// May be null if the account doesn't have a last name or it is not available to the bot.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// The attachment containing the profile picture of the account.
    /// <br/>
    /// May be null if the account doesn't have a profile picture or it is not available to the bot
    /// or it should be fetched manually via <see cref="IAccountService.GetAvatarAsync"/> method.
    /// </summary>
    public PhotoAttachment? Avatar { get; init; }

    /// <summary>
    /// The language of the account.
    /// <br/>
    /// May be null if the account doesn't have a language or it is not available to the bot.
    /// </summary>
    public CultureInfo? CultureInfo { get; init; }

    public IReadOnlyList<object> CustomData { get; init; } = ImmutableArray<object>.Empty;
}
