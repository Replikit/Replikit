using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Abstractions.Repositories.Exceptions;

public class ReplikitRepositoryException : ReplikitDomainException
{
    public ReplikitRepositoryException(string? message) : base(message) { }
}
