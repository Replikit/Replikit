using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneMessageHandler : MessageEventHandler<MessageReceivedEvent>
{
    private readonly ISceneStorageProvider _sceneStorageProvider;
    private readonly SceneHandlerAccessor _sceneHandlerAccessor;

    public SceneMessageHandler(ISceneStorageProvider sceneStorageProvider, SceneHandlerAccessor sceneHandlerAccessor)
    {
        _sceneStorageProvider = sceneStorageProvider;
        _sceneHandlerAccessor = sceneHandlerAccessor;
    }

    protected override async Task<Unit> HandleAsync(IEventContext<MessageReceivedEvent> context, NextAction next)
    {
        var sceneStorage = _sceneStorageProvider.Resolve();

        var sceneInstance = await sceneStorage.GetAsync(Channel.Id, CancellationToken);
        if (sceneInstance is null) return await next();

        var transition = sceneInstance.Transitions.FirstOrDefault(x => x.Text == Message.Text);

        var sceneRequest = transition is not null
            ? new SceneRequest(transition.Stage, true, Context, sceneInstance: sceneInstance)
            : new SceneRequest(sceneInstance.CurrentStage, false, Context, sceneInstance: sceneInstance);

        var sceneContext = new SceneContext(sceneRequest, ServiceProvider, cancellationToken: CancellationToken);
        await _sceneHandlerAccessor.Handler.Handle(sceneContext);

        return default;
    }
}
