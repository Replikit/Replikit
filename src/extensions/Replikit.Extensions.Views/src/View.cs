using Kantaiko.Controllers;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Handlers.Context;
using Replikit.Extensions.State;
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

    protected bool IsExternalActivation => Context.Request.EventContext is null;

    protected ViewRequest Request => Context.Request;

    protected IAdapterEventContext<ButtonPressedEvent> EventContext =>
        Request.EventContext ??
        throw new InvalidOperationException("Failed to access event context since view was activated externally");

    protected ButtonPressedEvent AdapterEvent => EventContext.Event;

    protected IAdapter Adapter => EventContext.Adapter;

    public virtual Task<ViewResult> RenderAsync(CancellationToken cancellationToken) =>
        Task.FromResult(Render());

    public virtual ViewResult Render() =>
        throw new NotImplementedException("You must implement Render or RenderAsync view method");

    internal bool UpdateRequested { get; private set; }

    protected static ViewMessageBuilder CreateBuilder() => new();
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
