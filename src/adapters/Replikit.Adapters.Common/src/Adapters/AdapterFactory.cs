using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Adapters.Common.Adapters;

public abstract class AdapterFactory<TOptions> : IAdapterFactory
{
    Type IAdapterFactory.OptionsType => typeof(TOptions);

    protected abstract string DisplayName { get; }

    protected virtual async Task<AccountInfo> GetBotAccountInfo(IAdapter adapter,
        CancellationToken cancellationToken = default)
    {
        return await adapter.Repository.GetAccountInfoAsync(adapter.Id.BotId, cancellationToken)
               ?? throw new InvalidOperationException("Failed to fetch bot account info");
    }

    protected virtual async Task<AdapterInfo> CreateAdapterInfoAsync(IAdapter adapter,
        CancellationToken cancellationToken = default)
    {
        var accountInfo = await GetBotAccountInfo(adapter, cancellationToken);

        return new AdapterInfo(adapter.Id, accountInfo, DisplayName);
    }

    protected abstract Task<Adapter> CreateAsync(TOptions options, AdapterFactoryContext context,
        CancellationToken cancellationToken = default);

    public async Task<IAdapter> CreateAsync(object options, AdapterFactoryContext context,
        CancellationToken cancellationToken)
    {
        var adapter = await CreateAsync((TOptions) options, context, cancellationToken);
        var adapterInfo = await CreateAdapterInfoAsync(adapter, cancellationToken);

        adapter.Info = adapterInfo;

        return adapter;
    }
}
