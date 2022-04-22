using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Examples.State.States;
using Replikit.Extensions.State;

namespace Replikit.Examples.State.Controllers;

public class SessionController : Controller
{
    private readonly IAccountState<TestState> _session;

    public SessionController(IAccountState<TestState> session)
    {
        _session = session;
    }

    [Command("count")]
    public OutMessage Count()
    {
        _session.Value.Count++;
        return $"Count: {_session.Value.Count}";
    }
}
