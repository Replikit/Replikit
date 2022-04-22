using System.Linq.Expressions;
using Kantaiko.Controllers;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Core.EntityCollections;
using Replikit.Extensions.Scenes.Messages;
using Replikit.Extensions.State;

namespace Replikit.Extensions.Scenes;

public abstract class Scene : ControllerBase<SceneContext>
{
    protected SceneRequest Request => Context.Request;

    protected IEventContext<MessageReceivedEvent> EventContext =>
        Request.EventContext ??
        throw new InvalidOperationException("Failed to access event context since scene was activated externally");

    protected MessageReceivedEvent Event => EventContext.Event;
    protected Message Message => Event.Message;

    protected ChannelInfo Channel => Event.Channel;
    protected AccountInfo Account => Event.Account;

    protected IMessageCollection MessageCollection => Context.MessageCollection;

    protected static SceneResult TransitionTo(Expression<Action> stage) => new(stage);
    protected static SceneResult TransitionTo(Expression<Func<Task>> stage) => new(stage);

    protected static SceneResult TransitionTo<TScene>(Expression<Action<TScene>> stage, OutMessage? message = null)
        where TScene : Scene => new(stage, message);

    protected static SceneResult TransitionTo<TScene>(Expression<Func<TScene, Task>> stage, OutMessage? message = null)
        where TScene : Scene => new(stage, message);

    protected static SceneResult Exit() => new(true);
    protected static SceneResult Exit(OutMessage message) => new(message, true);

    protected static SceneMessageBuilder CreateBuilder() => new();

    protected bool FirstTime => Request.FirstTime;

    [Stage]
    public SceneResult ExitStage() => Exit();

    [Stage]
    public SceneResult ExitWithMessageStage(OutMessage message) => Exit(message);
}

public abstract class Scene<TState> : Scene where TState : notnull, new()
{
    private readonly IState<TState> _state;

    protected Scene(IState<TState> state)
    {
        _state = state;
    }

    protected TState State => _state.Value;
    protected void ClearState() => _state.Clear();
}
