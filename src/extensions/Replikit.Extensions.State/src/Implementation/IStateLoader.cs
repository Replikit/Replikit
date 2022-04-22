namespace Replikit.Extensions.State.Implementation;

public interface IStateLoader
{
    Task LoadAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(CancellationToken cancellationToken = default);
}
