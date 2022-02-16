using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.Views.Internal;

internal class ViewRequestResult
{
    public ViewRequestResult(GlobalMessageIdentifier viewId)
    {
        ViewId = viewId;
    }

    public GlobalMessageIdentifier ViewId { get; }
}
