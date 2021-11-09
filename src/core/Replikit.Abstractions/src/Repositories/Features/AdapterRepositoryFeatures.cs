namespace Replikit.Abstractions.Repositories.Features;

[Flags]
public enum AdapterRepositoryFeatures
{
    None = 0,
    GetAccountInfo = 1 << 0,
    GetChannelInfo = 1 << 1,
    ResolveAttachmentUrl = 1 << 2,
    All = ~0
}
