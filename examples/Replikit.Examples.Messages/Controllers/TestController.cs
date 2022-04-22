using System.Text.Json;
using System.Text.Json.Serialization;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class TestController : Controller
{
    [Command("test attachments")]
    public OutMessage Test()
    {
        var message = JsonSerializer.Serialize(Message.Reply?.Attachments, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        });

        return OutMessage.FromCode(message);
    }
}
