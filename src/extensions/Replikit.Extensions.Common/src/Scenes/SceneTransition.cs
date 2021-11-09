namespace Replikit.Extensions.Common.Scenes;

public class SceneTransition
{
    private SceneTransition() { }

    public SceneTransition(string text, SceneStage stage)
    {
        Text = text;
        Stage = stage;
    }

    public string Text { get; private set; } = null!;
    public SceneStage Stage { get; private set; } = null!;
}
