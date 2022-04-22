namespace Replikit.Extensions.Scenes.Models;

public record SceneInstance(SceneInstanceStage CurrentStage, IReadOnlyList<SceneInstanceTransition> Transitions);
