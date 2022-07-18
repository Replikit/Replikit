using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Utils;
using Replikit.Core.Controllers.Configuration;
using Replikit.Core.Controllers.Configuration.Context;
using Replikit.Core.Controllers.Context;
using Replikit.Extensions.State.ExecutionHandlers;

namespace Replikit.Extensions.State.EventHandlers.ControllerConfiguration;

internal class AddStateLoadingHandler : ControllerConfigurationHandler<IMessageControllerContext>
{
    protected override void Handle(IControllerConfigurationContext<IMessageControllerContext> context)
    {
        Handlers.InsertAfter(
            x => x is InstantiateControllerHandler<IMessageControllerContext>,
            new LoadStateHandler()
        );
    }
}
