using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Views.Actions;

namespace Replikit.Extensions.Views;

public class ViewMessage : OutMessage
{
    public ViewActionMatrix Actions { get; set; } = new();
}
