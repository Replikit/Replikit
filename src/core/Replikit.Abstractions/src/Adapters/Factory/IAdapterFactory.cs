namespace Replikit.Abstractions.Adapters.Factory;

/// <summary>
/// Defines the mechanism for creating adapters.
/// </summary>
public interface IAdapterFactory
{
    /// <summary>
    /// The type of the options required by the adapter.
    /// </summary>
    Type OptionsType { get; }

    /// <summary>
    /// Creates an adapter.
    /// </summary>
    /// <param name="options">An instance of the adapter options.</param>
    /// <param name="context">A factory context.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the created adapter.
    /// </returns>
    Task<IAdapter> CreateAsync(object options, AdapterFactoryContext context,
        CancellationToken cancellationToken = default);
}
