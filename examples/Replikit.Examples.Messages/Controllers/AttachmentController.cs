using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class AttachmentController : Controller
{
    [Command("photo with text")]
    public OutMessage GetPhotoWithText() => new MessageBuilder()
        .AddText("Hi")
        .WithAttachment(PhotoAttachment.FromUrl("https://picsum.photos/512"));

    [Command("photo")]
    public OutMessage GetPhoto() => PhotoAttachment.FromUrl("https://picsum.photos/512");

    [Command("upload photo")]
    public async Task<OutMessage> UploadPhoto()
    {
        using var client = new HttpClient();
        var content = await client.GetStreamAsync("https://picsum.photos/512");

        return PhotoAttachment.FromContent(content, "test.png");
    }
}
