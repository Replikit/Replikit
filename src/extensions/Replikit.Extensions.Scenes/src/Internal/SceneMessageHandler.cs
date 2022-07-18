using Kantaiko.Routing;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Core.Handlers.Context;
using Replikit.Extensions.Scenes.Models;
using Replikit.Extensions.State;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneMessageHandler : MessageEventHandler<MessageReceivedEvent>
{
    private readonly SceneHandlerAccessor _sceneHandlerAccessor;
    private readonly IStateManager _stateManager;

    public SceneMessageHandler(SceneHandlerAccessor sceneHandlerAccessor, IStateManager stateManager)
    {
        _sceneHandlerAccessor = sceneHandlerAccessor;
        _stateManager = stateManager;
    }

    protected override async Task<Unit> HandleAsync(IChannelEventContext<MessageReceivedEvent> context, NextAction next)
    {
        var sceneState = await _stateManager.GetSceneStateAsync<SceneState>(Channel.Id, CancellationToken);

        if (!sceneState.HasValue || sceneState.Value.SceneInstance is not { } sceneInstance)
        {
            return await next();
        }

        var transition = sceneInstance.Transitions.FirstOrDefault(x => x.Text == Message.Text);

        var sceneRequest = transition is not null
            ? new SceneRequest(Channel.Id, transition.Stage, true, Context, sceneState)
            : new SceneRequest(Channel.Id, sceneInstance.CurrentStage, false, Context, sceneState);

        await using var scope = ServiceProvider.CreateAsyncScope();

        var sceneContext = new SceneContext(sceneRequest, scope.ServiceProvider, CancellationToken);
        await _sceneHandlerAccessor.Handler.HandleAsync(sceneContext, scope.ServiceProvider, CancellationToken);

        return default;
    }
}
