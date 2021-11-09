using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Adapters;

public interface IAdapterCollection
{
    IAdapter? Resolve(string type);
    IAdapter? Resolve(AdapterIdentifier identifier);
}
