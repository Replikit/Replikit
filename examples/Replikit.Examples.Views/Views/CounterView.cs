using Replikit.Abstractions.Messages.Builder;
using Replikit.Extensions.State;
using Replikit.Extensions.Views;

namespace Replikit.Examples.Views.Views;

public class CounterView : View
{
    private readonly IState<CounterState> _state;

    public CounterView(IState<CounterState> state)
    {
        _state = state;
    }

    [Action(AllowExternalActivation = true)]
    public void Increment(int step) => _state.Value.Increment(step);

    [Action]
    public void Clear() => _state.Clear();

    public override ViewResult Render()
    {
        return CreateBuilder()
            .AddText($"Count: {_state.Value.Count}")
            .AddAction("+1", () => Increment(1))
            .AddAction("+10", () => Increment(10))
            .AddAction("Clear", () => Clear());
    }
}
