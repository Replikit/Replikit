using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Adapters.Loader;

/// <summary>
/// Represents the adapter that should be loaded.
/// </summary>
public class AdapterDescriptor
{
    /// <summary>
    /// Creates a new instance of <see cref="AdapterDescriptor"/>.
    /// </summary>
    /// <param name="type">An adapter type.</param>
    /// <param name="options">An adapter options.</param>
    public AdapterDescriptor(string type, object options)
    {
        Check.NotNull(type);
        Check.NotNull(options);

        Type = type;
        Options = options;
    }

    /// <summary>
    /// The type of the adapter.
    /// </summary>
    public string Type { get; }

    /// <summary>
    /// The options of the adapter instance.
    /// </summary>
    public object Options { get; }
}
