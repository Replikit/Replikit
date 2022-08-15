using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class MentionController : Controller
{
    [Command("mention id")]
    public OutMessage MentionById(Identifier identifier)
    {
        return new MentionTextToken("baka", AccountId: identifier);
    }

    [Command("mention username")]
    public OutMessage MentionByUsername(string username)
    {
        return new MentionTextToken(username, Username: username);
    }
}
