namespace Replikit.Abstractions.Common.Exceptions;

public class UnexpectedlyUnsupportedFeatureException : ReplikitDomainException
{
    public UnexpectedlyUnsupportedFeatureException(object instance, Enum feature) : base(
        $"The {instance.GetType().Name} does not support {feature}, but the opposite has been reported. " +
        "Please make sure you are using the latest version of this adapter and then open an issue.") { }
}
