namespace Replikit.Abstractions.Management.Features;

[Flags]
public enum MemberPermissions
{
    None = 0,
    SendMessages = 1 << 0,
    EditMessages = 1 << 1,
    DeleteMessages = 1 << 2,
    DeleteAllMessages = 1 << 3,
    ManagePermissions = 1 << 4,
    ManageInfo = 1 << 5,
    AddUsers = 1 << 6,
    RemoveUsers = 1 << 7,
    All = ~0
}
