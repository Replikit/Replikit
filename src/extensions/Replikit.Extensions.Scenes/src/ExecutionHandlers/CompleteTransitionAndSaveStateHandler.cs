using System.Linq.Expressions;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Keyboard;
using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;
using Replikit.Core.Utils;
using Replikit.Extensions.Scenes.Models;
using Replikit.Extensions.State;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.Scenes.ExecutionHandlers;

internal class CompleteTransitionAndSaveStateHandler : ControllerExecutionHandler<SceneContext>
{
    protected override async Task<ControllerResult> HandleAsync(ControllerContext<SceneContext> context,
        NextAction next)
    {
        var sceneManager = context.ServiceProvider.GetRequiredService<ISceneManager>();
        var stateManager = context.ServiceProvider.GetRequiredService<IStateManager>();

        if (context.Result is not SceneResult sceneResult)
        {
            throw new InvalidOperationException("Unexpected scene processing result");
        }

        var sceneRequest = context.RequestContext.Request;
        var sceneState = sceneRequest.SceneState;
        var sceneInstance = sceneState?.Value.SceneInstance;

        var messageCollection = context.RequestContext.MessageCollection;

        SceneInstanceStage CreateSceneStage(Expression stage)
        {
            var (method, parameters) = MethodExpressionTransformer.Transform(stage);

            if (method.DeclaringType is null)
            {
                throw new InvalidOperationException($"Invalid stage method {method}");
            }

            var dynamicParameters = parameters.Select(x => new DynamicValue(x)).ToArray();

            return new SceneInstanceStage(method.DeclaringType.FullName!, method.ToString()!, dynamicParameters);
        }

        var channelId = sceneRequest.ChannelId;

        async Task ClearAssociatedStates()
        {
            var associatedStates = await stateManager.FindAllStatesAsync(
                q => q.Where(x => x.Key.StateType == StateType.State));

            foreach (var state in associatedStates)
            {
                if (state.Key.Type != typeof(SceneState))
                {
                    state.Clear();
                }
            }
        }

        if (sceneInstance is not null && sceneInstance.CurrentStage.Type != sceneRequest.Stage.Type)
        {
            // Clear states when moving to another scene
            await ClearAssociatedStates();
        }

        // If transition was requested, create new scene request and process it instead
        if (sceneResult.Transition is not null)
        {
            if (sceneResult.OutMessage is not null)
            {
                await messageCollection.SendAsync(sceneResult.OutMessage, cancellationToken: context.CancellationToken);
            }

            var transitionStage = CreateSceneStage(sceneResult.Transition);

            var newSceneInstance = new SceneInstance(sceneInstance?.CurrentStage ?? transitionStage,
                sceneInstance?.Transitions ?? Array.Empty<SceneInstanceTransition>());

            sceneState ??= await stateManager.GetSceneStateAsync<SceneState>(
                channelId, context.CancellationToken);

            sceneState.Value.SceneInstance = newSceneInstance;

            var transitionRequest = new SceneRequest(channelId, transitionStage, true,
                sceneRequest.EventContext, sceneState);

            await sceneManager.EnterSceneAsync(transitionRequest, context.CancellationToken);
            return ControllerResult.Empty;
        }

        OutMessage? outMessage = null;
        IReadOnlyList<SceneInstanceTransition> transitions;

        if (sceneResult.OutMessage is not null)
        {
            outMessage = sceneResult.OutMessage;
            transitions = Array.Empty<SceneInstanceTransition>();
        }
        else if (sceneResult.SceneMessageBuilder is not null)
        {
            var (message, sceneTransitions) = sceneResult.SceneMessageBuilder.BuildWithTransitions();
            outMessage = message;

            transitions = sceneTransitions.Select(transition =>
            {
                var stage = CreateSceneStage(transition.Stage);
                return new SceneInstanceTransition(transition.Text, stage);
            }).ToArray();
        }
        else
        {
            transitions = Array.Empty<SceneInstanceTransition>();
        }

        if (outMessage is not null)
        {
            if (outMessage.MessageKeyboard is not { ButtonMatrix.Rows.Count: > 0 })
            {
                outMessage = outMessage with { MessageKeyboard = MessageKeyboard.Remove };
            }

            await messageCollection.SendAsync(outMessage, cancellationToken: context.CancellationToken);
        }

        var stateLoader = context.ServiceProvider.GetRequiredService<IStateLoader>();

        if (sceneResult.ShouldExit)
        {
            sceneState?.Clear();

            await ClearAssociatedStates();
        }
        else
        {
            var newSceneInstance = new SceneInstance(sceneRequest.Stage, transitions);

            sceneState ??= await stateManager.GetSceneStateAsync<SceneState>(
                channelId, context.CancellationToken);

            sceneState.Value.SceneInstance = newSceneInstance;
        }

        await stateLoader.SaveAsync(context.CancellationToken);

        return ControllerResult.Empty;
    }
}
