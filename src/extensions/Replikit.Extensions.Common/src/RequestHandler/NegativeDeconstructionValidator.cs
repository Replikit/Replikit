using Kantaiko.Controllers.Design;

namespace Replikit.Extensions.Common.RequestHandler;

public class NegativeDeconstructionValidator : IDeconstructionValidator
{
    public bool CanDeconstruct(Type parameterType) => false;

    private static NegativeDeconstructionValidator? _instance;
    public static NegativeDeconstructionValidator Instance => _instance ??= new NegativeDeconstructionValidator();
}
