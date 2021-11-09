namespace Replikit.Adapters.Common.Exceptions;

public class AdapterInitializerNotSpecifiedException : ReplikitAdapterException
{
    public AdapterInitializerNotSpecifiedException() : base("Adapter initializer is not specified.") { }
}
