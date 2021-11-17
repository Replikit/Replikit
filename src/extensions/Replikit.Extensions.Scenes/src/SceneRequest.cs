using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes;

public class SceneRequest
{
    public SceneRequest(SceneStage stage, bool firstTime,
        IEventContext<MessageReceivedEvent>? eventContext = null,
        GlobalIdentifier? channelId = null,
        IMessageCollection? messageCollection = null,
        SceneInstance? sceneInstance = null)
    {
        Stage = stage;
        FirstTime = firstTime;
        ChannelId = channelId;
        EventContext = eventContext;
        MessageCollection = messageCollection;
        SceneInstance = sceneInstance;
    }

    public SceneStage Stage { get; }
    public bool FirstTime { get; }

    public IEventContext<MessageReceivedEvent>? EventContext { get; }
    public GlobalIdentifier? ChannelId { get; }
    public IMessageCollection? MessageCollection { get; }

    public SceneInstance? SceneInstance { get; }
}
