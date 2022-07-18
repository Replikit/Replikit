using Kantaiko.Properties;
using Kantaiko.Routing.Context;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.State.Implementation;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

public class ViewContext : AsyncContextBase, IHasStateKeyFactory
{
    public ViewContext(ViewRequest request,
        IServiceProvider? serviceProvider = null,
        CancellationToken cancellationToken = default) :
        base(serviceProvider, cancellationToken)
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

    public IStateKeyFactory StateKeyFactory => ViewStateKeyFactory.Instance;
}
