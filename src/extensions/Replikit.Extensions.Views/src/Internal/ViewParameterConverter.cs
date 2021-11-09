using Kantaiko.Controllers.Converters;
using Kantaiko.Controllers.Validation;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Extensions.Common.Utils;

namespace Replikit.Extensions.Views.Internal;

internal class ViewParameterConverter : IParameterConverter
{
    public bool CheckValueExistence(ParameterConversionContext context) => true;

    public ValidationResult Validate(ParameterConversionContext context) => ValidationResult.Success;

    public Task<IResolutionResult> Resolve(ParameterConversionContext context, CancellationToken cancellationToken)
    {
        var index = context.Info.Endpoint.Parameters.IndexOf(context.Info);
        var request = context.ServiceProvider.GetRequiredService<ViewRequestContextAccessor>().Context.Request!;

        var value = request.Parameters.Length > index ? request.Parameters[index] : null;
        var resolutionResult = ResolutionResult.Success(value);
        return Task.FromResult<IResolutionResult>(resolutionResult);
    }
}
