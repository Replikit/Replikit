namespace Replikit.Abstractions.Channels.Services;

/// <summary>
/// The features of <see cref="IChannelService"/>.
/// </summary>
[Flags]
public enum ChannelServiceFeatures
{
    /// <summary>
    /// The service does not support any features.
    /// </summary>
    None = 0,

    /// <summary>
    /// The service supports the <see cref="IChannelService.GetAsync"/> method.
    /// </summary>
    Get = 1 << 1,

    /// <summary>
    /// The service supports the <see cref="IChannelService.ChangeTitleAsync"/> method.
    /// </summary>
    ChangeTitle = 1 << 2,

    /// <summary>
    /// The service supports all the features.
    /// </summary>
    All = ~0,
}
