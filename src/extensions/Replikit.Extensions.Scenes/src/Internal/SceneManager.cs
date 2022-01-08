using Kantaiko.Routing.Context;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.EntityCollections;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneManager : ISceneManager
{
    private readonly IContextAccessor<IEventContext<IEvent>> _eventContext;
    private readonly SceneHandlerAccessor _handlerAccessor;
    private readonly IAdapterCollection _adapterCollection;
    private readonly IServiceProvider _serviceProvider;

    public SceneManager(IContextAccessor<IEventContext<IEvent>> eventContext, SceneHandlerAccessor handlerAccessor,
        IAdapterCollection adapterCollection, IServiceProvider serviceProvider)
    {
        _eventContext = eventContext;
        _handlerAccessor = handlerAccessor;
        _adapterCollection = adapterCollection;
        _serviceProvider = serviceProvider;
    }

    internal async Task ProcessRequest(SceneRequest sceneRequest, CancellationToken cancellationToken = default)
    {
        IServiceProvider serviceProvider;
        CancellationToken requestCancellationToken;

        if (sceneRequest.EventContext is not null)
        {
            requestCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(
                sceneRequest.EventContext.CancellationToken,
                cancellationToken).Token;
            serviceProvider = sceneRequest.EventContext.ServiceProvider;
        }
        else
        {
            requestCancellationToken = cancellationToken;
            serviceProvider = _serviceProvider;
        }

        var sceneContext = new SceneContext(sceneRequest, serviceProvider, cancellationToken: requestCancellationToken);
        await _handlerAccessor.Handler.Handle(sceneContext);
    }

    public async Task EnterSceneAsync(SceneStage stage, CancellationToken cancellationToken = default)
    {
        if (_eventContext.Context is not IEventContext<MessageReceivedEvent> context)
        {
            throw new InvalidOperationException(
                "Cannot enter the scene outside of the message event context");
        }

        var sceneRequest = new SceneRequest(stage, true, context);
        await ProcessRequest(sceneRequest, cancellationToken);
    }

    public async Task EnterSceneAsync(GlobalIdentifier channelId, SceneStage stage,
        CancellationToken cancellationToken = default)
    {
        var adapter = _adapterCollection.ResolveRequired(channelId.AdapterId);
        var messageCollection = new MessageCollection(channelId, adapter.MessageService);

        var sceneRequest = new SceneRequest(stage, true, channelId: channelId, messageCollection: messageCollection);
        await ProcessRequest(sceneRequest, cancellationToken);
    }
}
