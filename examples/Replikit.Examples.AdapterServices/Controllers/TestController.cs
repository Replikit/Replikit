using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Examples.AdapterServices.Views;
using Replikit.Extensions.Views;

namespace Replikit.Examples.AdapterServices.Controllers;

internal class TestController : Controller
{
    private readonly IViewManager _viewManager;

    public TestController(IViewManager viewManager)
    {
        _viewManager = viewManager;
    }

    [Pattern("answer-view")]
    public Task SendAnswerView()
    {
        return _viewManager.SendViewAsync<AnswerView>(Channel.Id);
    }
}
