using System.ComponentModel;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Common.Exceptions;

/// <summary>
/// Represents an error occured during the functioning of the Replikit framework and its components.
/// </summary>
public class ReplikitException : Exception
{
    /// <summary>
    /// Creates a new instance of <see cref="ReplikitException"/> with the specified message.
    /// The message is always required.
    /// </summary>
    /// <param name="message">An error message.</param>
    public ReplikitException([Localizable(true)] string message) : base(Check.NotNull(message)) { }

    /// <summary>
    /// Creates a new instance of <see cref="ReplikitException"/> with the specified message and inner exception.
    /// The message is always required.
    /// </summary>
    /// <param name="message">An error message.</param>
    /// <param name="innerException">An inner exception.</param>
    public ReplikitException([Localizable(true)] string message, Exception? innerException) :
        base(Check.NotNull(message), innerException) { }
}
