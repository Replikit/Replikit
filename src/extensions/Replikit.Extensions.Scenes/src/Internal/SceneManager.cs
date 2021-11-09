using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneManager : ISceneManager
{
    private readonly IEventContextAccessor _eventContextAccessor;
    private readonly SceneRequestHandlerAccessor _requestHandlerAccessor;
    private readonly SceneRequestContextAccessor _sceneRequestContextAccessor;

    public SceneManager(IEventContextAccessor eventContextAccessor, SceneRequestHandlerAccessor requestHandlerAccessor,
        SceneRequestContextAccessor sceneRequestContextAccessor)
    {
        _eventContextAccessor = eventContextAccessor;
        _requestHandlerAccessor = requestHandlerAccessor;
        _sceneRequestContextAccessor = sceneRequestContextAccessor;
    }

    internal async Task ProcessRequest(SceneRequest sceneRequest, CancellationToken cancellationToken = default)
    {
        var requestCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(
            sceneRequest.EventContext.CancellationToken,
            cancellationToken).Token;

        var sceneContext = new SceneContext(sceneRequest, sceneRequest.EventContext.ServiceProvider,
            requestCancellationToken);
        _sceneRequestContextAccessor.Context = sceneContext;

        await _requestHandlerAccessor.RequestHandler.HandleAsync(sceneContext,
            sceneRequest.EventContext.ServiceProvider,
            requestCancellationToken);
    }

    public async Task EnterScene(SceneStage stage, CancellationToken cancellationToken = default)
    {
        if (_eventContextAccessor.Context is not IEventContext<MessageReceivedEvent> context)
        {
            throw new InvalidOperationException(
                "Cannot enter the scene outside of the message event context");
        }

        var sceneRequest = new SceneRequest(stage, true, context);
        await ProcessRequest(sceneRequest, cancellationToken);
    }
}
