using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Adapters.Common.Exceptions;

public class UnsafeMessageIdentifierException : ReplikitException
{
    public UnsafeMessageIdentifierException(MessageIdentifier messageIdentifier) : base(
        $"Message identifier {messageIdentifier} was expected to be safe global") { }
}
