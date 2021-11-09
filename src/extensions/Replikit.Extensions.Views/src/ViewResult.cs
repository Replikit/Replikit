using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Views.Messages;

namespace Replikit.Extensions.Views;

public class ViewResult
{
    public ViewResult(ViewMessageBuilder viewMessageBuilder)
    {
        ViewMessageBuilder = viewMessageBuilder;
    }

    public ViewResult(OutMessage? outMessage)
    {
        OutMessage = outMessage;
    }

    public ViewMessageBuilder? ViewMessageBuilder { get; }
    public OutMessage? OutMessage { get; }

    public static implicit operator ViewResult(OutMessage outMessage) => new(outMessage);
    public static implicit operator ViewResult(MessageBuilder messageBuilder) => new(messageBuilder);
    public static implicit operator ViewResult(ViewMessageBuilder viewMessageBuilder) => new(viewMessageBuilder);
}
