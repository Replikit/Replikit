using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Common.Exceptions;

/// <summary>
/// Represents an error occurred when feature that was reported as supported is not supported.
/// <br/>
/// The main reason for this exception is invalid implementation of the service by the adapter.
/// The client should not handle this exception and report it to the adapter developer.
/// </summary>
public class UnexpectedlyUnsupportedFeatureException : ReplikitException
{
    /// <summary>
    /// Creates a new instance of the <see cref="UnexpectedlyUnsupportedFeatureException"/>.
    /// </summary>
    /// <param name="instance">A service with invalid implementation.</param>
    /// <param name="feature">A feature that that was reported as supported, but is not supported.</param>
    public UnexpectedlyUnsupportedFeatureException(object instance, Enum feature) :
        base(string.Format(Strings.UnexpectedlyUnsupportedFeature, instance.GetType().Name, feature)) { }
}
