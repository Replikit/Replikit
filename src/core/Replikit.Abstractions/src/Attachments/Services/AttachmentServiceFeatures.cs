namespace Replikit.Abstractions.Attachments.Services;

/// <summary>
/// The features of <see cref="IAttachmentService"/>.
/// </summary>
[Flags]
public enum AttachmentServiceFeatures
{
    /// <summary>
    /// The service does not support any features.
    /// </summary>
    None = 0,

    /// <summary>
    /// The service supports the <see cref="IAttachmentService.GetAsync"/> method.
    /// </summary>
    Get = 1 << 0,

    /// <summary>
    /// The service supports the <see cref="IAttachmentService.GetContentAsync"/> method.
    /// </summary>
    GetContent = 1 << 1,

    /// <summary>
    /// The service supports the <see cref="IAttachmentService.GetUrlAsync"/> method.
    /// </summary>
    GetUrl = 1 << 2,

    /// <summary>
    /// The service supports all the features.
    /// </summary>
    All = ~0
}
