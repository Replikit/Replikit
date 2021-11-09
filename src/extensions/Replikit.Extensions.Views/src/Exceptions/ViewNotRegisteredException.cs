using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Extensions.Views.Exceptions;

public class ViewNotRegisteredException : ReplikitException
{
    public ViewNotRegisteredException(string typeName) : base($"View with type name {typeName} is not registered") { }
}
