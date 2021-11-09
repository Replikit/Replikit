using Replikit.Abstractions.Messages.Builder;
using Replikit.Extensions.Scenes;

namespace Replikit.Examples.Scenes.Scenes;

public class MainScene : Scene
{
    [Stage]
    public SceneResult MainStage()
    {
        return CreateBuilder()
            .AddText("Choose next scene")
            .AddTransition<CounterScene>(0, "Counter", x => x.MainStage())
            .AddTransition<FormScene>(0, "Form", x => x.NameStage())
            .AddTransition(1, "Exit", () => ExitWithMessageStage("Bye"));
    }
}
