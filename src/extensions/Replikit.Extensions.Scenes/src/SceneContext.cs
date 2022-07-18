using Kantaiko.Properties.Immutable;
using Kantaiko.Routing.Context;
using Replikit.Core.EntityCollections;
using Replikit.Extensions.Scenes.Internal;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.Scenes;

public class SceneContext : AsyncContextBase, IHasStateKeyFactory
{
    public SceneContext(SceneRequest request,
        IServiceProvider? serviceProvider = null,
        CancellationToken cancellationToken = default) :
        base(serviceProvider, cancellationToken)
    {
        Request = request;
    }

    public SceneRequest Request { get; }

    private IMessageCollection? _messageCollection;

    public IMessageCollection MessageCollection =>
        _messageCollection ??= Core.EntityCollections.MessageCollection.Create(Request.ChannelId, ServiceProvider);

    IStateKeyFactory IHasStateKeyFactory.StateKeyFactory => SceneStateKeyFactory.Instance;
}
