using Kantaiko.Routing.Events;
using Replikit.Core.Controllers.Configuration.Context;
using Replikit.Core.Controllers.Context;

namespace Replikit.Core.Controllers.Configuration;

public interface IMessageControllerConfiguration
{
    event SyncEventHandler<IControllerConfigurationContext<IMessageControllerContext>> Configuring;
}
