using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Adapters.Common.Exceptions;

public class ReplikitAdapterException : ReplikitException
{
    public ReplikitAdapterException(string? message) : base(message) { }
}
