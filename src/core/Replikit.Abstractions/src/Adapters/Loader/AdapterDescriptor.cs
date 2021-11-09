namespace Replikit.Abstractions.Adapters.Loader;

public class AdapterDescriptor
{
    public AdapterDescriptor(string type, object options)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(options);

        Type = type;
        Options = options;
    }

    public string Type { get; }
    public object Options { get; }
}
