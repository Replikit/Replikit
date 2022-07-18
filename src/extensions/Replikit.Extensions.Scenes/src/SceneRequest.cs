using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers.Context;
using Replikit.Extensions.Scenes.Models;
using Replikit.Extensions.State;

namespace Replikit.Extensions.Scenes;

public class SceneRequest
{
    public SceneRequest(
        GlobalIdentifier channelId,
        SceneInstanceStage stage,
        bool firstTime,
        IChannelEventContext<MessageReceivedEvent>? eventContext = null,
        IState<SceneState>? sceneState = null)
    {
        ChannelId = channelId;
        Stage = stage;
        FirstTime = firstTime;
        EventContext = eventContext;
        SceneState = sceneState;
    }

    public GlobalIdentifier ChannelId { get; }
    public SceneInstanceStage Stage { get; }
    public bool FirstTime { get; }

    public IChannelEventContext<MessageReceivedEvent>? EventContext { get; }
    public IState<SceneState>? SceneState { get; }
}
