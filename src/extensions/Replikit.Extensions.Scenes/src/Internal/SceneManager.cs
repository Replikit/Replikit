using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneManager : ISceneManager
{
    private readonly IEventContextAccessor _eventContextAccessor;
    private readonly SceneRequestHandlerAccessor _requestHandlerAccessor;
    private readonly SceneRequestContextAccessor _sceneRequestContextAccessor;
    private readonly IAdapterCollection _adapterCollection;
    private readonly IServiceProvider _serviceProvider;

    public SceneManager(IEventContextAccessor eventContextAccessor, SceneRequestHandlerAccessor requestHandlerAccessor,
        SceneRequestContextAccessor sceneRequestContextAccessor, IAdapterCollection adapterCollection,
        IServiceProvider serviceProvider)
    {
        _eventContextAccessor = eventContextAccessor;
        _requestHandlerAccessor = requestHandlerAccessor;
        _sceneRequestContextAccessor = sceneRequestContextAccessor;
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

        var sceneContext = new SceneContext(sceneRequest, serviceProvider, requestCancellationToken);
        _sceneRequestContextAccessor.Context = sceneContext;

        await _requestHandlerAccessor.RequestHandler.HandleAsync(sceneContext,
            serviceProvider, requestCancellationToken);
    }

    public async Task EnterSceneAsync(SceneStage stage, CancellationToken cancellationToken = default)
    {
        if (_eventContextAccessor.Context is not IEventContext<MessageReceivedEvent> context)
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
