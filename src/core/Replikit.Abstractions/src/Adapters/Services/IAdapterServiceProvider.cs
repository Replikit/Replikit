namespace Replikit.Abstractions.Adapters.Services;

public interface IAdapterServiceProvider
{
    object? GetService(Type serviceType);
}
