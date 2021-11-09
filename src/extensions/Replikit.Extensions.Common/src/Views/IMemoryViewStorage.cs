using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.Common.Views;

public interface IMemoryViewStorage : IViewStorage
{
    IReadOnlyDictionary<MessageIdentifier, ViewInstance> Views { get; }
}
