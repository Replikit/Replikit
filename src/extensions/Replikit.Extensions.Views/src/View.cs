using Kantaiko.Controllers;
using Replikit.Abstractions.Messages.Events;
using Replikit.Extensions.Views.Internal;
using Replikit.Extensions.Views.Messages;

namespace Replikit.Extensions.Views;

public abstract class View : ControllerBase<ViewContext>
{
    protected void Update()
    {
        UpdateRequested = true;
    }

    [Action]
    public void Init() => Update();

    protected IServiceProvider ServiceProvider => Context.ServiceProvider;
    protected CancellationToken CancellationToken => Context.CancellationToken;

    protected ViewRequest Request => Context.Request;
    protected ButtonPressedEvent? Event => Request?.Event;

    public virtual Task<ViewResult> RenderAsync(CancellationToken cancellationToken) =>
        Task.FromResult(Render());

    public virtual ViewResult Render() =>
        throw new NotImplementedException("You must implement Render or RenderAsync view method");

    public virtual Task<ViewResult> RenderClosedAsync(CancellationToken cancellationToken) =>
        Task.FromResult(RenderClosed());

    public virtual ViewResult RenderClosed() => Render();

    internal bool UpdateRequested { get; private set; }

    protected static ViewMessageBuilder CreateBuilder() => new();
}

public abstract class View<TState> : View, IStatefulView where TState : notnull
{
    protected TState State { get; set; } = default!;

    Type IStatefulView.StateType => typeof(TState);
    object IStatefulView.StateValue => State;

    void IStatefulView.SetState(object state)
    {
        State = (TState) state;
    }
}
