using Kantaiko.Controllers.Converters;
using Kantaiko.Controllers.Validation;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.Common.Utils;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneParameterConverter : IParameterConverter
{
    public bool CheckValueExistence(ParameterConversionContext context) => true;

    public ValidationResult Validate(ParameterConversionContext context) => ValidationResult.Success;

    public Task<IResolutionResult> Resolve(ParameterConversionContext context, CancellationToken cancellationToken)
    {
        var index = context.Info.Endpoint.Parameters.IndexOf(context.Info);
        var request = context.ServiceProvider.GetRequiredService<SceneRequestContextAccessor>().Context.Request;

        var value = request.Stage.Parameters.Length > index ? request.Stage.Parameters[index] : null;
        var resolutionResult = ResolutionResult.Success(value);
        return Task.FromResult<IResolutionResult>(resolutionResult);
    }
}
