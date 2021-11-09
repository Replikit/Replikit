using Replikit.Abstractions.Messages.Builder;
using Replikit.Extensions.Scenes;

namespace Replikit.Examples.Scenes.Scenes;

public class CounterScene : Scene<CounterScene.CounterState>
{
    public class CounterState
    {
        public int Count { get; set; }
    }

    [Stage]
    public SceneResult MainStage()
    {
        if (Message.Text == "+1")
        {
            State.Count++;
        }

        return CreateBuilder()
            .AddText($"Count: {State.Count}")
            .WithKeyboard(b => b.AddButton("+1"))
            .AddTransition<MainScene>(0, "Back to the main scene", x => x.MainStage());
    }
}
