using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Factory;

namespace Replikit.Adapters.Common.Adapters;

public abstract class AdapterFactory<TOptions> : IAdapterFactory
{
    Type IAdapterFactory.OptionsType => typeof(TOptions);

    protected abstract Task<Adapter> CreateAsync(TOptions options, AdapterFactoryContext context,
        CancellationToken cancellationToken = default);

    public async Task<IAdapter> CreateAsync(object options, AdapterFactoryContext context,
        CancellationToken cancellationToken)
    {
        var adapter = await CreateAsync((TOptions) options, context, cancellationToken);

        await adapter.InitializeCoreAsync(cancellationToken);

        return adapter;
    }
}
