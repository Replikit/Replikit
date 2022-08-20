using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Resources;

namespace Replikit.Abstractions.Adapters.Exceptions;

/// <summary>
/// Represents an error occurred when adapter service is not implemented.
/// </summary>
public class AdapterServiceNotImplementedException : ReplikitException
{
    /// <summary>
    /// Creates a new instance of <see cref="AdapterServiceNotImplementedException"/>.
    /// </summary>
    /// <param name="adapter">An adapter which does not implement the service.</param>
    /// <param name="serviceType">A type of service which is not implemented.</param>
    public AdapterServiceNotImplementedException(IAdapter adapter, Type serviceType) :
        base(string.Format(Strings.AdapterServiceNotImplemented, serviceType, adapter.GetType().FullName)) { }
}
