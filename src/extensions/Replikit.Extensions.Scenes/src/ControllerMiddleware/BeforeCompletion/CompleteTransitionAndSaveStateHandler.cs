using System.Diagnostics;
using System.Linq.Expressions;
using Kantaiko.Controllers.Middleware;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Keyboard;
using Replikit.Extensions.Common.Models;
using Replikit.Extensions.Common.Scenes;
using Replikit.Extensions.Common.Utils;
using Replikit.Extensions.Scenes.Internal;

namespace Replikit.Extensions.Scenes.ControllerMiddleware.BeforeCompletion;

internal class CompleteTransitionAndSaveStateHandler : EndpointMiddleware<SceneContext>
{
    private readonly ISceneStorageProvider _sceneStorageProvider;
    private readonly SceneManager _sceneManager;

    public CompleteTransitionAndSaveStateHandler(ISceneStorageProvider sceneStorageProvider, SceneManager sceneManager)
    {
        _sceneStorageProvider = sceneStorageProvider;
        _sceneManager = sceneManager;
    }

    public override EndpointMiddlewareStage Stage => EndpointMiddlewareStage.BeforeCompletion;

    public override async Task HandleAsync(EndpointMiddlewareContext<SceneContext> context,
        CancellationToken cancellationToken)
    {
        var controller = context.ExecutionContext.ControllerInstance;
        Debug.Assert(controller is not null);

        if (controller is not Scene scene)
        {
            throw new InvalidOperationException($"Controller of type \"{controller.GetType().Name}\" is not a Scene");
        }

        var result = context.ExecutionContext.ProcessingResult?.ReturnValue;
        if (result is not SceneResult sceneResult)
        {
            throw new InvalidOperationException("Unexpected scene processing result");
        }

        var sceneRequest = context.RequestContext.Request;
        var sceneInstance = sceneRequest.SceneInstance;
        var sceneStorage = _sceneStorageProvider.Resolve();

        var statefulScene = scene as IStatefulScene;
        var channelId = context.RequestContext.ChannelId;

        var messageCollection = context.RequestContext.MessageCollection;

        SceneStage CreateSceneStage(Expression stage)
        {
            var (method, parameters) = MethodExpressionTransformer.Transform(stage);

            if (method.DeclaringType is null)
            {
                throw new InvalidOperationException($"Invalid stage method {method}");
            }

            return new SceneStage(method.DeclaringType.FullName!, method.ToString()!, parameters);
        }

        // If transition was requested, create new scene request and process it instead
        if (sceneResult.Transition is not null)
        {
            if (sceneResult.OutMessage is not null)
            {
                await messageCollection.SendAsync(sceneResult.OutMessage, cancellationToken: cancellationToken);
            }

            var transitionStage = CreateSceneStage(sceneResult.Transition);

            var state = new DynamicValue(statefulScene?.StateValue);

            var newSceneInstance = new SceneInstance(channelId, state, sceneInstance?.CurrentStage ?? transitionStage,
                sceneInstance?.Transitions ?? Array.Empty<SceneTransition>());

            var transitionRequest = new SceneRequest(transitionStage, true,
                sceneRequest.EventContext, sceneRequest.ChannelId, sceneRequest.MessageCollection, newSceneInstance);

            await _sceneManager.ProcessRequest(transitionRequest, cancellationToken);
            return;
        }

        OutMessage? outMessage = null;
        IReadOnlyList<SceneTransition> transitions;

        if (sceneResult.OutMessage is not null)
        {
            outMessage = sceneResult.OutMessage;
            transitions = Array.Empty<SceneTransition>();
        }
        else if (sceneResult.SceneMessageBuilder is not null)
        {
            var (message, sceneTransitions) = sceneResult.SceneMessageBuilder.BuildWithTransitions();
            outMessage = message;

            transitions = sceneTransitions.Select(transition =>
            {
                var stage = CreateSceneStage(transition.Stage);
                return new SceneTransition(transition.Text, stage);
            }).ToArray();
        }
        else
        {
            transitions = Array.Empty<SceneTransition>();
        }

        if (outMessage is not null)
        {
            if (outMessage.MessageKeyboard is not { ButtonMatrix.Rows.Count: > 0 })
            {
                outMessage = outMessage with { MessageKeyboard = MessageKeyboard.Remove };
            }

            await messageCollection.SendAsync(outMessage, cancellationToken: cancellationToken);
        }

        if (sceneResult.ShouldExit)
        {
            await sceneStorage.DeleteAsync(channelId, cancellationToken);
        }
        else
        {
            var state = new DynamicValue(statefulScene?.StateValue);

            var newSceneInstance = new SceneInstance(channelId, state, sceneRequest.Stage, transitions);
            await sceneStorage.SetAsync(channelId, newSceneInstance, cancellationToken);
        }
    }
}
