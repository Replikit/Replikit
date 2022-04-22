namespace Replikit.Abstractions.Adapters.Exceptions;

public class AdapterServiceNotImplementedException : Exception
{
    public AdapterServiceNotImplementedException(object instance, Type serviceType) : base(
        $"Service of type {serviceType} is not implemented by {instance.GetType().FullName}") { }
}
