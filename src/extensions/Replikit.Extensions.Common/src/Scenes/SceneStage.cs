namespace Replikit.Extensions.Common.Scenes;

public class SceneStage
{
    private SceneStage() { }

    public SceneStage(string sceneType, string method, object[] parameters)
    {
        SceneType = sceneType;
        Method = method;
        Parameters = parameters;
    }

    public string SceneType { get; private set; } = null!;
    public string Method { get; private set; } = null!;
    public object[] Parameters { get; private set; } = null!;
}
