namespace Replikit.Abstractions.Attachments.Exceptions;

public class InvalidPhotoAttachmentException : ReplikitAttachmentException
{
    public InvalidPhotoAttachmentException()
        : base("Unable to construct PhotoAttachment with empty photo size collection") { }
}
