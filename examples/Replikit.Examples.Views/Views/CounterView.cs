using Replikit.Extensions.State;
using Replikit.Extensions.Views;

namespace Replikit.Examples.Views.Views;

public class CounterView : View<CounterState>
{
    public CounterView(IState<CounterState> state) : base(state) { }

    public override ViewMessage Render() => new()
    {
        Text = $"Count: {State.Count}",
        Actions =
        {
            {
                Action("+1", c => Increment(1)),
                Action("+10", c => Increment(10)),
                Action("+100", c => Increment(100))
            },
            {
                Action("-1", c => Increment(-1)),
                Action("-10", c => Increment(-10)),
                Action("-100", c => Increment(-100))
            },

            Action("Clear", c => Clear())
        }
    };

    public void Increment(int step) => State.Increment(step);

    private void Clear() => ClearState();
}
