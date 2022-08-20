using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Common.Exceptions;

/// <summary>
/// Represents an error occurred when feature that was called is not supported by the service.
/// </summary>
public class UnsupportedFeatureException : ReplikitException
{
    /// <summary>
    /// Creates a new instance of <see cref="UnsupportedFeatureException"/>.
    /// </summary>
    /// <param name="instance">A service that has unsupported feature.</param>
    /// <param name="feature">A feature that is not supported.</param>
    public UnsupportedFeatureException(object instance, Enum feature) :
        base(string.Format(Strings.UnsupportedFeatureException, instance.GetType().Name, feature)) { }
}
