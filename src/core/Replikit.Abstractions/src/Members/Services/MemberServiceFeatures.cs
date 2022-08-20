namespace Replikit.Abstractions.Members.Services;

/// <summary>
/// The features of <see cref="IMemberService"/>.
/// </summary>
[Flags]
public enum MemberServiceFeatures
{
    /// <summary>
    /// The service does not support any features.
    /// </summary>
    None = 0,

    /// <summary>
    /// The service supports the <see cref="IMemberService.GetAsync"/> method.
    /// </summary>
    Get = 1 << 0,

    /// <summary>
    /// The service supports the <see cref="IMemberService.GetManyAsync"/> method.
    /// </summary>
    GetMany = 1 << 1,

    /// <summary>
    /// The service supports the <see cref="IMemberService.AddAsync"/> method.
    /// </summary>
    Add = 1 << 2,

    /// <summary>
    /// The service supports the <see cref="IMemberService.RemoveAsync"/> method.
    /// </summary>
    Remove = 1 << 3,

    /// <summary>
    /// The service supports the <see cref="IMemberService.GetTotalCountAsync"/> method.
    /// </summary>
    GetTotalCount = 1 << 4,

    /// <summary>
    /// The service supports all the features.
    /// </summary>
    All = ~0,
}
