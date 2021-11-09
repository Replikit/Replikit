using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Adapters.Exceptions;

public class AdapterNotFoundException : ReplikitException
{
    public AdapterNotFoundException(string name) : base($"Adapter with name {name} not found") { }

    public AdapterNotFoundException(AdapterIdentifier identifier) : base(
        $"Adapter with identifier {identifier} not found") { }
}
