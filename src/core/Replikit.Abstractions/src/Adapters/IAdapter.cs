using Replikit.Abstractions.Accounts.Services;
using Replikit.Abstractions.Adapters.Exceptions;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Channels.Services;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Members.Services;
using Replikit.Abstractions.Messages.Services;

namespace Replikit.Abstractions.Adapters;

/// <summary>
/// Defines a unified contract for any platform or social network.
/// <br/>
/// The adapter consists of services that provide access to the platform's API.
/// <br/>
/// Multiple adapters even of the same type can be registered in the bot.
/// </summary>
public interface IAdapter
{
    /// <summary>
    /// The service provider containing all adapter services.
    /// <br/>
    /// In most cases, client code should not use this property.
    /// </summary>
    IServiceProvider AdapterServices { get; }

    /// <summary>
    /// The <see cref="AdapterInfo"/> associated with this adapter.
    /// </summary>
    AdapterInfo AdapterInfo { get; }

    /// <summary>
    /// The <see cref="PlatformInfo"/> associated with the platform implemented by this adapter.
    /// </summary>
    PlatformInfo PlatformInfo { get; }

    /// <summary>
    /// The information about the bot authenticated by this adapter.
    /// </summary>
    AdapterBotInfo BotInfo { get; }

    /// <summary>
    /// The <see cref="IAdapterEventSource"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    IAdapterEventSource AdapterEventSource { get; }

    /// <summary>
    /// The <see cref="ITextFormatter"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    ITextFormatter TextFormatter { get; }

    /// <summary>
    /// The <see cref="ITextTokenizer"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    ITextTokenizer TextTokenizer { get; }

    /// <summary>
    /// The <see cref="IMessageService"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    IMessageService MessageService { get; }

    /// <summary>
    /// The <see cref="IAccountService"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    IAccountService AccountService { get; }

    /// <summary>
    /// The <see cref="IChannelService"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    IChannelService ChannelService { get; }

    /// <summary>
    /// The <see cref="IMemberService"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    IMemberService MemberService { get; }

    /// <summary>
    /// The <see cref="IAttachmentService"/> of the adapter.
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException">The service is not implemented.</exception>
    IAttachmentService AttachmentService { get; }
}
