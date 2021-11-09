using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Examples.Sessions.Sessions;
using Replikit.Extensions.Sessions;

namespace Replikit.Examples.Sessions.Controllers;

public class SessionController : Controller
{
    private readonly IAccountSession<TestSession> _session;

    public SessionController(IAccountSession<TestSession> session)
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
