using Replikit.Abstractions.Adapters;

namespace Replikit.Adapters.Common.Adapters;

public abstract class AdapterFactory<TOptions> : IAdapterFactory
{
    Type IAdapterFactory.OptionsType => typeof(TOptions);

    protected Task<IAdapter> CreateAsync(TOptions options, AdapterContext context,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Create(options, context));
    }

    protected virtual IAdapter Create(TOptions options, AdapterContext context)
    {
        throw new InvalidOperationException("Adapter factory must implement Create or CreateAsync method");
    }

    Task<IAdapter> IAdapterFactory.CreateAsync(object options, AdapterContext context,
        CancellationToken cancellationToken)
    {
        return CreateAsync((TOptions) options, context, cancellationToken);
    }
}
