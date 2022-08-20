using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Factory;

namespace Replikit.Adapters.Common.Adapters;

public abstract class AdapterFactory<TOptions> : IAdapterFactory
{
    Type IAdapterFactory.OptionsType => typeof(TOptions);

    protected abstract AdapterInfo AdapterInfo { get; }

    protected abstract PlatformInfo PlatformInfo { get; }

    protected abstract Task<Adapter> CreateAsync(TOptions options, AdapterInfo adapterInfo, PlatformInfo platformInfo,
        AdapterFactoryContext context, CancellationToken cancellationToken = default);

    public async Task<IAdapter> CreateAsync(object options, AdapterFactoryContext context,
        CancellationToken cancellationToken)
    {
        var adapter = await CreateAsync((TOptions) options, AdapterInfo, PlatformInfo, context, cancellationToken);

        await adapter.InitializeCoreAsync(cancellationToken);

        return adapter;
    }
}
