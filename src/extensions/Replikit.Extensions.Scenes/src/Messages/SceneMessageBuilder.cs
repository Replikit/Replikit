using System.Linq.Expressions;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.Scenes.Messages;

public class SceneMessageBuilder : MessageBuilder<SceneMessageBuilder>
{
    private readonly List<SceneTransition> _transitions = new();

    public SceneMessageBuilder AddTransitionRow(params SceneTransition[] transitions)
    {
        _transitions.AddRange(transitions);
        KeyboardBuilder.AddButtonRow(transitions.Select(x => x.Text).ToArray());

        return this;
    }

    public SceneMessageBuilder AddTransition(int row, string text, Expression<Action> stage)
    {
        return AddTransitionInternal(row, text, stage);
    }

    public SceneMessageBuilder AddTransition(int row, string text, Expression<Func<Task>> stage)
    {
        return AddTransitionInternal(row, text, stage);
    }

    public SceneMessageBuilder AddTransition(string text, Expression<Action> stage)
    {
        return AddTransitionInternal(text, stage);
    }

    public SceneMessageBuilder AddTransition(string text, Expression<Func<Task>> stage)
    {
        return AddTransitionInternal(text, stage);
    }

    public SceneMessageBuilder AddTransition<TScene>(int row, string text, Expression<Action<TScene>> stage)
        where TScene : Scene
    {
        return AddTransitionInternal(row, text, stage);
    }

    public SceneMessageBuilder AddTransition<TScene>(int row, string text, Expression<Func<TScene, Task>> stage)
        where TScene : Scene
    {
        return AddTransitionInternal(row, text, stage);
    }

    public SceneMessageBuilder AddTransition<TScene>(string text, Expression<Action<TScene>> stage)
        where TScene : Scene
    {
        return AddTransitionInternal(text, stage);
    }

    public SceneMessageBuilder AddTransition<TScene>(string text, Expression<Func<TScene, Task>> stage)
        where TScene : Scene
    {
        return AddTransitionInternal(text, stage);
    }

    private SceneMessageBuilder AddTransitionInternal(int row, string text, Expression stage)
    {
        _transitions.Add(new SceneTransition(text, stage));
        KeyboardBuilder.AddButton(row, text);

        return this;
    }

    private SceneMessageBuilder AddTransitionInternal(string text, Expression stage)
    {
        _transitions.Add(new SceneTransition(text, stage));
        KeyboardBuilder.AddButton(text);

        return this;
    }

    internal (OutMessage, IReadOnlyList<SceneTransition>) BuildWithTransitions()
    {
        return (Build(), _transitions);
    }
}
