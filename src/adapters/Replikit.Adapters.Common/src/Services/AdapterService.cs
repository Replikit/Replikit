using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Adapters.Common.Services;

public abstract class AdapterService
{
    protected AdapterService(IAdapter adapter)
    {
        Adapter = adapter;
    }

    protected IAdapter Adapter { get; }

    protected GlobalIdentifier CreateGlobalIdentifier(Identifier identifier)
    {
        return new GlobalIdentifier(Adapter.BotInfo.Id, identifier);
    }
}
