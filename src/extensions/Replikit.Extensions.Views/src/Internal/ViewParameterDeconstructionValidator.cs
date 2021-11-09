using Kantaiko.Controllers.Design;

namespace Replikit.Extensions.Views.Internal;

internal class ViewParameterDeconstructionValidator : IDeconstructionValidator
{
    public bool CanDeconstruct(Type parameterType) => false;
}
