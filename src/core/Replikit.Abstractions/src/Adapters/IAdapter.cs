using Replikit.Abstractions.Adapters.Exceptions;
using Replikit.Abstractions.Adapters.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Management.Services;
using Replikit.Abstractions.Messages.Services;
using Replikit.Abstractions.Repositories.Services;

namespace Replikit.Abstractions.Adapters;

public interface IAdapter : IAdapterServiceProvider
{
    AdapterIdentifier Id { get; }

    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    AdapterInfo Info { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    IEventSource EventSource { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    IAdapterRepository Repository { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    ITextFormatter TextFormatter { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    ITextTokenizer TextTokenizer { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    IMessageService MessageService { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    IMemberService MemberService { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="AdapterServiceNotImplementedException"></exception>
    IChannelService ChannelService { get; }
}
