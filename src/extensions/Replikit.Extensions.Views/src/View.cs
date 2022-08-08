﻿using Kantaiko.Controllers;
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

    public virtual Task<ViewResult> RenderAsync(CancellationToken cancellationToken) =>
        Task.FromResult(Render());

    public virtual ViewResult Render() =>
        throw new NotImplementedException("You must implement Render or RenderAsync view method");

    internal bool UpdateRequested { get; private set; }
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
