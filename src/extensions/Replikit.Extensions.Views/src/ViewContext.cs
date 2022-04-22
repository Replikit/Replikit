using Kantaiko.Properties;
using Kantaiko.Routing.Context;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.Views;

public class ViewContext : ContextBase
{
    public ViewContext(ViewRequest request,
        IServiceProvider? serviceProvider = null,
        IReadOnlyPropertyCollection? properties = null,
        CancellationToken cancellationToken = default) :
        base(serviceProvider, properties, cancellationToken)
    {
        Request = request;

        if (request.ViewState is { Key: var key })
        {
            var channelId = new GlobalIdentifier(key.AdapterId!, key.ChannelId!.Value);
            MessageId = new GlobalMessageIdentifier(channelId, key.MessageId!.Value);
        }
    }

    public ViewRequest Request { get; }
    public GlobalMessageIdentifier? MessageId { get; internal set; }
}
