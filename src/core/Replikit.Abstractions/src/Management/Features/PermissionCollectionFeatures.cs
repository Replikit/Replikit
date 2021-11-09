namespace Replikit.Abstractions.Management.Features;

[Flags]
public enum PermissionCollectionFeatures
{
    None = 0,
    GetPermissions = 1 << 0,
    CheckPermissions = 1 << 1,
    AddPermissions = 1 << 2,
    RemovePermissions = 1 << 3,
    All = ~0
}
