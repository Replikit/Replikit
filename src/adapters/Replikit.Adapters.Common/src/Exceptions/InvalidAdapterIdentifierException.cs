using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Adapters.Common.Exceptions;

public class InvalidAdapterIdentifierException : ReplikitException
{
    public InvalidAdapterIdentifierException(AdapterIdentifier expected, AdapterIdentifier actual)
        : base($"Invalid adapter identifier. Expected {expected}, got {actual}") { }
}
