using Replikit.Abstractions.Messages.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Extensions.Views.Actions;

public interface IViewActionContext : IAdapterEventContext<ButtonPressedEvent>
{
    void SuppressAutoUpdate();
    void Update();
}
