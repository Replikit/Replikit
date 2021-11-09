namespace Replikit.Abstractions.Common.Exceptions;

public class UnsupportedFeatureException : ReplikitDomainException
{
    public UnsupportedFeatureException(object instance, Enum feature) : base(
        $"The {instance.GetType().Name} does not support {feature}") { }
}
