using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Management.Features;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Repositories.Features;

namespace Replikit.Abstractions.Adapters;

public interface IAdapter : IHasFeatures<AdapterFeatures>
{
    AdapterIdentifier Id { get; }

    string DisplayName { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="UnsupportedFeatureException"></exception>
    IEventSource EventSource { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="UnsupportedFeatureException"></exception>
    IAdapterRepository Repository { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="UnsupportedFeatureException"></exception>
    ITextFormatter TextFormatter { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="UnsupportedFeatureException"></exception>
    ITextTokenizer TextTokenizer { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="UnsupportedFeatureException"></exception>
    IMessageService MessageService { get; }

    /// <summary>
    /// </summary>
    /// <exception cref="UnsupportedFeatureException"></exception>
    IMemberService MemberService { get; }
}
