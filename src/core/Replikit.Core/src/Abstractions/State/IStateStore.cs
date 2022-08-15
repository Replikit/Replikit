using Replikit.Core.Common;

namespace Replikit.Core.Abstractions.State;

/// <summary>
/// The service which can be used to query and persist instances of <see cref="StateItem"/> in the database.
/// This service is singleton and could cache some items globally.
/// </summary>
public interface IStateStore
{
    /// <summary>
    /// The <see cref="IQueryExecutor"/> associated with this store.
    /// </summary>
    IQueryExecutor QueryExecutor { get; }

    /// <summary>
    /// Creates a new state item query which can modified and executed using <see cref="QueryExecutor"/>.
    /// </summary>
    /// <returns>A state item query that can be modified and executed.</returns>
    IQueryable<StateItem> CreateQuery();

    /// <summary>
    /// Saves specified state items to the database.
    /// <list type="bullet">
    /// <item>Items with the same key and different values will be replaced;</item>
    /// <item>Items with new keys will be created;</item>
    /// <item>Items with null values will be deleted.</item>
    /// </list>
    /// </summary>
    /// <param name="items">A enumerable of state items to save.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the save operation.</returns>
    Task SaveManyAsync(IEnumerable<StateItem> items, CancellationToken cancellationToken = default);
}
