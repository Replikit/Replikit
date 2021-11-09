namespace Replikit.Abstractions.Adapters;

public interface IAdapterFactory
{
    Type OptionsType { get; }

    Task<IAdapter> CreateAsync(object options, AdapterContext context, CancellationToken cancellationToken = default);
}
