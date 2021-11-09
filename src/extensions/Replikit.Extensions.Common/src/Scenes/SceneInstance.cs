using Replikit.Abstractions.Common.Models;
using Replikit.Extensions.Common.Models;

namespace Replikit.Extensions.Common.Scenes;

public class SceneInstance
{
    private SceneInstance() { }

    public SceneInstance(Identifier channelId, DynamicValue state,
        SceneStage currentStage,
        IReadOnlyList<SceneTransition> transitions)
    {
        ChannelId = channelId;
        State = state;
        CurrentStage = currentStage;
        Transitions = transitions;
    }

    public Identifier ChannelId { get; } = null!;
    public DynamicValue State { get; private set; } = null!;

    public SceneStage CurrentStage { get; private set; } = null!;
    public IReadOnlyList<SceneTransition> Transitions { get; private set; } = null!;
}
