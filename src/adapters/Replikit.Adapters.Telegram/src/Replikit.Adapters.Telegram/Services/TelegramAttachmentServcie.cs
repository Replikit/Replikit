using System.Diagnostics;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Common.Models;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace Replikit.Adapters.Telegram.Services;

internal class TelegramAttachmentService : IAttachmentService
{
    private readonly ITelegramBotClient _backend;
    private readonly string _botToken;

    public TelegramAttachmentService(ITelegramBotClient backend, string botToken)
    {
        _backend = backend;
        _botToken = botToken;
    }

    public AttachmentServiceFeatures Features =>
        AttachmentServiceFeatures.GetUrl;

    public async Task<Uri?> GetUrlAsync(Identifier attachmentId, CancellationToken cancellationToken = default)
    {
        try
        {
            var file = await _backend.GetFileAsync(attachmentId, cancellationToken);

            Debug.Assert(file.FilePath is not null);
            return CreateFileUrl(file.FilePath);
        }
        catch (ApiRequestException e) when (e.Message.Contains("file is temporarily unavailable"))
        {
            return null;
        }
    }

    private Uri CreateFileUrl(string path)
    {
        return new Uri($"https://api.telegram.org/file/bot{_botToken}/{path}");
    }
}
