using Kantaiko.Properties.Immutable;
using Kantaiko.Routing.Context;
using Replikit.Core.EntityCollections;

namespace Replikit.Extensions.Scenes;

public class SceneContext : ContextBase
{
    public SceneContext(SceneRequest request,
        IServiceProvider? serviceProvider = null,
        IImmutablePropertyCollection? properties = null,
        CancellationToken cancellationToken = default) :
        base(serviceProvider, properties, cancellationToken)
    {
        Request = request;
    }

    public SceneRequest Request { get; }

    private IMessageCollection? _messageCollection;

    public IMessageCollection MessageCollection =>
        _messageCollection ??= Core.EntityCollections.MessageCollection.Create(Request.ChannelId!, ServiceProvider);
}
