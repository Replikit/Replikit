namespace Replikit.Abstractions.Management.Services;

[Flags]
public enum MemberServiceFeatures
{
    None = 0,
    Add = 1 << 0,
    Remove = 1 << 1,
    GetMany = 1 << 2,
    ListMany = 1 << 3,
    GetTotalCount = 1 << 4,
    All = ~0,
}
