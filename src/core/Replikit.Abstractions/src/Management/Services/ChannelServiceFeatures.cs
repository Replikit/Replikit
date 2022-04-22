namespace Replikit.Abstractions.Management.Services;

[Flags]
public enum ChannelServiceFeatures
{
    None = 0,
    ChangeTitle = 1 << 0,
    ChangePhoto = 1 << 1,
    All = ~0,
}
