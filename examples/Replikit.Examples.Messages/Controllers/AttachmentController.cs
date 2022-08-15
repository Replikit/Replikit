using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.Messages.Controllers;

public class AttachmentController : Controller
{
    [Command("photo with text")]
    public OutMessage GetPhotoWithText()
    {
        return new OutMessage
        {
            Text = "hi",
            Attachments =
            {
                OutAttachment.FromUrl(AttachmentType.Photo, "https://picsum.photos/512")
            }
        };
    }

    [Command("sticker")]
    public OutMessage GetSticker(string uploadId)
    {
        return OutAttachment.FromUploadId(AttachmentType.Sticker, uploadId);
    }

    [Command("photo")]
    public OutMessage GetPhoto()
    {
        return OutAttachment.FromUrl(AttachmentType.Photo, "https://picsum.photos/512");
    }

    [Command("upload photo")]
    public async Task<OutMessage> UploadPhoto()
    {
        using var client = new HttpClient();
        var content = await client.GetStreamAsync("https://picsum.photos/512");

        return OutAttachment.FromContent(AttachmentType.Photo, content);
    }
}
