using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Examples.Views.Views;
using Replikit.Extensions.Views;

namespace Replikit.Examples.Views.Controllers;

public class CounterController : Controller
{
    private readonly IViewManager _viewManager;

    public CounterController(IViewManager viewManager)
    {
        _viewManager = viewManager;
    }

    [Command("counter")]
    public Task CreateCounter()
    {
        return _viewManager.SendViewAsync<CounterView>(MessageCollection);
    }
}
