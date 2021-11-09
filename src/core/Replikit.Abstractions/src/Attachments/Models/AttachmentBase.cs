using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public abstract record AttachmentBase<T>(
    GlobalIdentifier? Id = null,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null
) : Attachment(Id, Caption, Url, FileName, Content) where T : Attachment, new()
{
    public static T FromUrl(string url,
        string? fileName = null,
        string? caption = null,
        GlobalIdentifier? id = null) =>
        new() { Id = id, Url = url, FileName = fileName ?? Path.GetFileName(url), Caption = caption };

    public static T FromContent(Stream content,
        string? fileName = null,
        string? caption = null,
        GlobalIdentifier? id = null) =>
        new() { Id = id, FileName = fileName, Content = content, Caption = caption };

    public static T FromFile(string path,
        string? fileName = null,
        string? caption = null,
        GlobalIdentifier? id = null) =>
        FromContent(File.OpenRead(path), fileName ?? Path.GetFileName(path), caption, id);
}
