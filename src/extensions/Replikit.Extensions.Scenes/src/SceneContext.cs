using Kantaiko.Routing.Context;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Extensions.Scenes;

public class SceneContext : ContextBase
{
    public SceneContext(SceneRequest request, IServiceProvider serviceProvider, CancellationToken cancellationToken) :
        base(serviceProvider, cancellationToken)
    {
        Request = request;
        MessageCollection = request.EventContext.GetMessageCollection();
    }

    public SceneRequest Request { get; }
    public IMessageCollection MessageCollection { get; }
}
