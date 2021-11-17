using Kantaiko.Routing.Context;
using Replikit.Abstractions.Common.Models;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Extensions.Scenes;

public class SceneContext : ContextBase
{
    public SceneContext(SceneRequest request, IServiceProvider serviceProvider, CancellationToken cancellationToken) :
        base(serviceProvider, cancellationToken)
    {
        Request = request;

        ChannelId = request.EventContext?.Event.Channel.Id ?? request.ChannelId ??
            throw new InvalidOperationException("SceneRequest must contain EventContext or ChannelId");

        MessageCollection = request.EventContext?.GetMessageCollection() ?? request.MessageCollection ??
            throw new InvalidOperationException("SceneRequest must contain EventContext or MessageCollection");
    }

    public SceneRequest Request { get; }
    public GlobalIdentifier ChannelId { get; }
    public IMessageCollection MessageCollection { get; }
}
