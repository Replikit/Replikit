namespace Replikit.Abstractions.Management.Features;

[Flags]
public enum MemberCollectionFeatures
{
    None = 0,
    Add = 1 << 0,
    Remove = 1 << 1,
    GetMany = 1 << 2,
    ListMany = 1 << 3,
    All = ~0,
}
