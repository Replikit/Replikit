using Replikit.Core.Abstractions.State;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Examples.Views.Views;
using Replikit.Extensions.Views;
using Replikit.Extensions.Views.Models;

namespace Replikit.Examples.Views.Controllers;

public class CounterController : Controller
{
    private readonly IViewManager _viewManager;

    public CounterController(IViewManager viewManager)
    {
        _viewManager = viewManager;
    }

    [Command("create counter")]
    public Task CreateCounter()
    {
        return _viewManager.SendViewAsync<CounterView>(Channel.Id);
    }

    [Command("increment all")]
    public async Task IncrementAllCounters()
    {
        var counters = await _viewManager.FindByStateAsync<CounterState>();

        async Task ActivateCounter(StateItem<ViewState> counter)
        {
            await _viewManager.ActivateAsync<CounterView>(counter, x => x.Increment(1));
        }

        await Task.WhenAll(counters.Select(ActivateCounter));
    }
}
