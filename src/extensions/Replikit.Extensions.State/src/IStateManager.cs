using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State;

public interface IStateManager
{
    IEnumerable<IState> LoadedStates { get; }

    Task<IState<TValue>> GetStateAsync<TValue>(StateKey key, CancellationToken cancellationToken = default)
        where TValue : notnull, new();

    Task<IReadOnlyList<IState>> FindStatesAsync(PartialStateKey partialKey,
        CancellationToken cancellationToken = default);
}
