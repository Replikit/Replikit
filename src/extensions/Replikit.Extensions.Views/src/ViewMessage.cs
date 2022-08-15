using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Views.Actions;

namespace Replikit.Extensions.Views;

public class ViewMessage : OutMessage
{
    public IViewActionMatrix Actions { get; set; } = new ViewActionMatrix();
}
