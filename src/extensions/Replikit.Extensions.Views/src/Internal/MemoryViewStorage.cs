using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Replikit.Abstractions.Messages.Models;
using Replikit.Extensions.Common.Views;

namespace Replikit.Extensions.Views.Internal;

internal class MemoryViewStorage : IMemoryViewStorage
{
    public const string Name = "memory";

    private readonly ConcurrentDictionary<MessageIdentifier, ViewInstance> _views = new();

    public IReadOnlyDictionary<MessageIdentifier, ViewInstance> Views => _views;

    public Task<ViewInstance?> FindAsync(object filter, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Unsupported filter type");
    }

    public Task<IReadOnlyList<ViewInstance>> FindManyAsync(object filter, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Unsupported filter type");
    }

    public void Set(MessageIdentifier messageId, ViewInstance viewInstance)
    {
        _views[messageId] = viewInstance;
    }

    public void Delete(MessageIdentifier messageId)
    {
        (_views as IDictionary<MessageIdentifier, ViewInstance>).Remove(messageId);
    }

    public Task<ViewInstance?> GetAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_views.GetValueOrDefault(messageId));
    }

#pragma warning disable 1998
    public async IAsyncEnumerable<ViewInstance> GetAllByTypeAsync(string typeName,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var viewInstance in _views.Values)
        {
            if (viewInstance.Type == typeName) yield return viewInstance;
        }
    }
#pragma warning restore 1998

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
}
