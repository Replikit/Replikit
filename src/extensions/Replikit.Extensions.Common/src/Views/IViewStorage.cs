using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.Common.Views;

public interface IViewStorage
{
    Task<ViewInstance?> GetAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default);
    IAsyncEnumerable<ViewInstance> GetAllByTypeAsync(string typeName, CancellationToken cancellationToken = default);

    Task<ViewInstance?> FindAsync(object filter, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ViewInstance>> FindManyAsync(object filter, CancellationToken cancellationToken = default);

    void Set(MessageIdentifier messageId, ViewInstance viewInstance);
    void Delete(MessageIdentifier messageId);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
