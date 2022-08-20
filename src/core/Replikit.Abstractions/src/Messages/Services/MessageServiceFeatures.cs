namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// The features of <see cref="IMessageService"/>.
/// </summary>
[Flags]
public enum MessageServiceFeatures
{
    /// <summary>
    /// No features are supported.
    /// </summary>
    None = 0,

    /// <summary>
    /// The service supports <see cref="IMessageService.SendAsync"/> method.
    /// </summary>
    Send = 1 << 0,

    /// <summary>
    /// The service supports <see cref="IMessageService.EditAsync"/> method with attachments.
    /// </summary>
    Edit = 1 << 1,

    /// <summary>
    /// The service supports <see cref="IMessageService.DeleteSingleAsync"/> method.
    /// </summary>
    DeleteSingle = 1 << 2,

    /// <summary>
    /// The service supports <see cref="IMessageService.DeleteManyAsync"/> method.
    /// </summary>
    DeleteMany = 1 << 3,

    /// <summary>
    /// The service supports <see cref="IMessageService.GetAsync"/> method.
    /// </summary>
    Get = 1 << 4,

    /// <summary>
    /// The service supports <see cref="IMessageService.GetManyAsync"/> method.
    /// </summary>
    GetMany = 1 << 5,

    /// <summary>
    /// The service supports <see cref="IMessageService.PinAsync"/> method.
    /// </summary>
    Pin = 1 << 6,

    /// <summary>
    /// The service supports <see cref="IMessageService.UnpinAsync"/> method.
    /// </summary>
    Unpin = 1 << 7,

    /// <summary>
    /// The service supports all the features.
    /// </summary>
    All = ~0
}
