using Replikit.Core.Abstractions.State;
using Replikit.Core.Common;

namespace Replikit.Extensions.State;

/// <summary>
/// This service is responsible for managing states in the active scope.
/// </summary>
public interface IStateManager
{
    /// <summary>
    /// Local states, loaded by the state manager.
    /// </summary>
    IEnumerable<IState> LoadedStates { get; }

    /// <summary>
    /// Finds states of all types using specified query builder.
    /// </summary>
    /// <param name="queryBuilder"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<IState>> FindAllStatesAsync(QueryBuilder<StateItem> queryBuilder,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds states of specified type using specified query builder.
    /// </summary>
    /// <param name="queryBuilder"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    Task<IReadOnlyList<IState<TValue>>> FindStatesAsync<TValue>(QueryBuilder<StateItem<TValue>>? queryBuilder = null,
        CancellationToken cancellationToken = default)
        where TValue : class, new();

    /// <summary>
    /// Finds an existing state by the specified key, or creates a new one.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    Task<IState<TValue>> GetStateAsync<TValue>(StateKey key, CancellationToken cancellationToken = default)
        where TValue : notnull, new();
}
