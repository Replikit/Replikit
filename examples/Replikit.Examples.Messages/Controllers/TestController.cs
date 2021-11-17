using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class TestController : Controller
{
    [Command("test")]
    public OutMessage Test()
    {
        var message = JsonSerializer.Serialize(Message, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        });

        return OutMessage.FromCode(message);
    }
}
