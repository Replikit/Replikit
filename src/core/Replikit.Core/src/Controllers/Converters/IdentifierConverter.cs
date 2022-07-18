using Kantaiko.Controllers.ParameterConversion;
using Kantaiko.Controllers.ParameterConversion.Text;
using Replikit.Abstractions.Common.Models;
using Replikit.Core.Resources;

namespace Replikit.Core.Controllers.Converters;

public class IdentifierConverter : SingleTextParameterConverter<Identifier>
{
    protected override ResolutionResult<Identifier> Resolve(TextParameterConversionContext context, string value)
    {
        if (long.TryParse(value, out var longId))
        {
            return ResolutionResult.Success(new Identifier(longId));
        }

        if (Guid.TryParse(value, out var guidId))
        {
            return ResolutionResult.Success(new Identifier(guidId));
        }

        if (!string.IsNullOrEmpty(value))
        {
            return ResolutionResult.Success(new Identifier(value));
        }

        return ResolutionResult.Error(Locale.InvalidIdentifier);
    }
}
