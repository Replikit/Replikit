using Kantaiko.Routing;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneMessageAdapterHandler : MessageAdapterEventHandler<MessageReceivedEvent>
{
    private readonly ISceneStorageProvider _sceneStorageProvider;
    private readonly SceneRequestHandlerAccessor _sceneRequestHandlerAccessor;
    private readonly SceneRequestContextAccessor _sceneRequestContextAccessor;

    public SceneMessageAdapterHandler(ISceneStorageProvider sceneStorageProvider,
        SceneRequestHandlerAccessor sceneRequestHandlerAccessor,
        SceneRequestContextAccessor sceneRequestContextAccessor)
    {
        _sceneStorageProvider = sceneStorageProvider;
        _sceneRequestHandlerAccessor = sceneRequestHandlerAccessor;
        _sceneRequestContextAccessor = sceneRequestContextAccessor;
    }

    public override async Task<Unit> HandleAsync(IEventContext<MessageReceivedEvent> context, NextAction next)
    {
        var sceneStorage = _sceneStorageProvider.Resolve();

        var sceneInstance = await sceneStorage.GetAsync(Channel.Id, CancellationToken);
        if (sceneInstance is null) return await next();

        var transition = sceneInstance.Transitions.FirstOrDefault(x => x.Text == Message.Text);

        var sceneRequest = transition is not null
            ? new SceneRequest(transition.Stage, true, Context, sceneInstance: sceneInstance)
            : new SceneRequest(sceneInstance.CurrentStage, false, Context, sceneInstance: sceneInstance);

        var sceneContext = new SceneContext(sceneRequest, ServiceProvider, CancellationToken);
        _sceneRequestContextAccessor.Context = sceneContext;

        await _sceneRequestHandlerAccessor.RequestHandler.HandleAsync(sceneContext, ServiceProvider, CancellationToken);

        return default;
    }
}
