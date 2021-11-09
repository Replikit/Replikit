using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes;

public class SceneRequest
{
    public SceneRequest(SceneStage stage, bool firstTime, IEventContext<MessageReceivedEvent> eventContext,
        SceneInstance? sceneInstance = null)
    {
        Stage = stage;
        FirstTime = firstTime;
        EventContext = eventContext;
        SceneInstance = sceneInstance;
    }

    public SceneStage Stage { get; }
    public bool FirstTime { get; }

    public IEventContext<MessageReceivedEvent> EventContext { get; }

    public SceneInstance? SceneInstance { get; }
}
