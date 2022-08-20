namespace Replikit.Abstractions.Accounts.Services;

/// <summary>
/// The features of <see cref="IAccountService"/>.
/// </summary>
[Flags]
public enum AccountServiceFeatures
{
    /// <summary>
    /// The service does not support any features.
    /// </summary>
    None = 0,

    /// <summary>
    /// The service supports the <see cref="IAccountService.GetAsync"/> method.
    /// </summary>
    Get = 1 << 0,

    /// <summary>
    /// The service supports the <see cref="IAccountService.GetAvatarAsync"/> method.
    /// </summary>
    GetAvatar = 1 << 1,

    /// <summary>
    /// The service supports all the features.
    /// </summary>
    All = ~0
}
