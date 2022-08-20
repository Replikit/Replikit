using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Keyboard;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class KeyboardController : Controller
{
    [Command("keyboard")]
    public OutMessage GetKeyboard() => new()
    {
        Text = "Test keyboard",
        Keyboard =
        {
            { "Test 1", "Test 2" },
            { "Test 3", "Test 4" }
        }
    };

    [Command("remove keyboard")]
    public OutMessage RemoveKeyboard() => new()
    {
        Text = "Test keyboard removed",
        Keyboard = MessageKeyboard.Remove
    };
}
