using Replikit.Abstractions.Messages.Builder;
using Replikit.Extensions.Scenes;
using Replikit.Extensions.State;

namespace Replikit.Examples.Scenes.Scenes;

public class FormScene : Scene<FormScene.FormState>
{
    public class FormState
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
    }

    public FormScene(IState<FormState> state) : base(state) { }

    [Stage]
    public SceneResult NameStage()
    {
        if (FirstTime || string.IsNullOrEmpty(Message.Text))
        {
            return "What's your name?";
        }

        State.Name = Message.Text;

        return TransitionTo(() => AgeStage());
    }

    [Stage]
    public SceneResult AgeStage()
    {
        if (FirstTime || string.IsNullOrEmpty(Message.Text))
        {
            return "What's your age?";
        }

        if (!int.TryParse(Message.Text, out var age))
        {
            return "Please enter your age, nothing else";
        }

        if (age < 18)
        {
            return TransitionTo<MainScene>(x => x.MainStage(), "Sorry, but this form is only for adults");
        }

        State.Age = age;

        return TransitionTo(() => GenderStage());
    }

    [Stage]
    public SceneResult GenderStage()
    {
        if (FirstTime || string.IsNullOrEmpty(Message.Text))
        {
            return CreateBuilder()
                .AddText("What's your gender?")
                .WithKeyboard(b => b.AddButtonRow("Male", "Female"));
        }

        State.Gender = Message.Text;

        return TransitionTo(() => FinalStage());
    }

    [Stage]
    public SceneResult FinalStage()
    {
        return CreateBuilder()
            .AddTextLine($"Name: {State.Name}")
            .AddTextLine($"Age: {State.Age}")
            .AddTextLine($"Gender: {State.Gender}")
            .AddTransition(0, "Fill again", () => NameStage())
            .AddTransition<MainScene>(0, "Back to the main scene", x => x.MainStage());
    }
}
