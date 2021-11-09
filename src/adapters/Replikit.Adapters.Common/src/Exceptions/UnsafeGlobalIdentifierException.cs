using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Adapters.Common.Exceptions;

public class UnsafeGlobalIdentifierException : ReplikitException
{
    public UnsafeGlobalIdentifierException(GlobalIdentifier globalIdentifier) : base(
        $"Identifier {globalIdentifier.Value} was expected to be safe global") { }
}
