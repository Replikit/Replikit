using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace ApplicationTemplate.Controllers;

public class HelloController : Controller
{
    [Pattern("hi")]
    public OutMessage Hello() => $"Hello, {Account.Username}!";
}
