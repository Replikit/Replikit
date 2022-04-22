namespace Replikit.Abstractions.Adapters;

public interface IAdapterFactory
{
    Type OptionsType { get; }

    Task<IAdapter> CreateAsync(object options, AdapterFactoryContext context,
        CancellationToken cancellationToken = default);
}
