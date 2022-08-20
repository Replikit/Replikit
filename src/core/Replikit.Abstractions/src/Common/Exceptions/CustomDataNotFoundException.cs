using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Common.Exceptions;

/// <summary>
/// Represents an error occured while accessing the custom data that is not associated with a specific object.
/// </summary>
public class CustomDataNotFoundException : ReplikitException
{
    /// <summary>
    /// Creates a new instance of <see cref="CustomDataNotFoundException"/>.
    /// </summary>
    /// <param name="dataType">A type of the custom data object that was not found.</param>
    public CustomDataNotFoundException(Type dataType) :
        base(string.Format(Strings.CustomDataNotFound, dataType.FullName)) { }
}
