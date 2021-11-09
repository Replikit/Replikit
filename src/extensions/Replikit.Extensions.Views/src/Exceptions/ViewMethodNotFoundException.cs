using Replikit.Abstractions.Common.Exceptions;

namespace Replikit.Extensions.Views.Exceptions;

public class ViewMethodNotFoundException : ReplikitException
{
    public ViewMethodNotFoundException(string viewType, string methodName) : base(
        $"Method \"{methodName}\" not found in view \"{viewType}\"") { }
}
