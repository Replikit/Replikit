using System.Linq.Expressions;
using Kantaiko.Controllers;
using Replikit.Extensions.State;
using Replikit.Extensions.Views.Actions;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

public abstract class View : IAutoRegistrableController<InternalViewContext>
{
    public virtual Task<ViewMessage> RenderAsync(CancellationToken cancellationToken) =>
        Task.FromResult(Render());

    public virtual ViewMessage Render() =>
        throw new NotImplementedException("You must implement Render or RenderAsync view method");

    protected static ViewAction Action(string text, Expression<Func<IViewActionContext, object>> action) =>
        new(text, action);

    protected static ViewAction Action(string text, Expression<Action<IViewActionContext>> action) =>
        new(text, action);
}

public abstract class View<TState> : View where TState : notnull, new()
{
    private readonly IState<TState> _state;

    protected View(IState<TState> state)
    {
        _state = state;
    }

    protected TState State => _state.Value;
    protected void ClearState() => _state.Clear();
}
