using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class KeyboardController : Controller
{
    [Command("keyboard")]
    public OutMessage GetKeyboard() => new MessageBuilder()
        .AddText("Test keyboard")
        .WithKeyboard(keyboard => keyboard
            .AddButtonRow("Test 1", "Test 2")
            .AddButtonRow()
            .AddButton("Test 3")
            .AddButton("Test 4"));

    [Command("remove keyboard")]
    public OutMessage RemoveKeyboard() => new MessageBuilder()
        .AddText("Test keyboard removed")
        .WithKeyboard(keyboard => keyboard.Remove());
}
