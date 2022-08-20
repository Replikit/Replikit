using Replikit.Abstractions.Accounts.Models;
using Replikit.Abstractions.Accounts.Services;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Core.GlobalServices;

internal class GlobalAccountService : IGlobalAccountService
{
    private readonly IAdapterCollection _adapterCollection;

    public GlobalAccountService(IAdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    private IAccountService ResolveAccountService(GlobalIdentifier channelId)
    {
        return _adapterCollection.ResolveRequired(channelId).AccountService;
    }

    public AccountServiceFeatures GetFeatures(BotIdentifier botId)
    {
        return _adapterCollection.ResolveRequired(botId).AccountService.Features;
    }

    public Task<AccountInfo?> GetAsync(GlobalIdentifier accountId, CancellationToken cancellationToken = default)
    {
        return ResolveAccountService(accountId).GetAsync(accountId, cancellationToken);
    }
}
