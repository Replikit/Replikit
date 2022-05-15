using System.Text.RegularExpressions;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Tokens;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class HelloController : Controller
{
    [Regex("^hi$", RegexOptions.IgnoreCase)]
    public OutMessage Greet() => $"Hello, {Account.Username}";

    [Command("greet bold")]
    public OutMessage GreetBold() => new MessageBuilder()
        .AddText("Hello, ")
        .AddText(Account.Username!, TextTokenModifiers.Bold);
}
