using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Replikit.Core.Common;
using Replikit.Extensions.Views.Actions;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class LoadViewActionParametersHandler : IControllerExecutionHandler<InternalViewContext>
{
    public Task HandleAsync(ControllerExecutionContext<InternalViewContext> context)
    {
        if (context.RequestContext.ActionRequest is not { } actionRequest)
        {
            return Task.CompletedTask;
        }

        var parameters = context.Endpoint!.Parameters;

        var constructedParameters = new object?[parameters.Count];
        var realParameterIndex = 0;

        for (var index = 0; index < context.Endpoint.Parameters.Count; index++)
        {
            var endpointParameter = context.Endpoint.Parameters[index];

            if (endpointParameter.ParameterType == typeof(IViewActionContext))
            {
                constructedParameters[index] = context.RequestContext.ViewActionContext!;
                continue;
            }

            constructedParameters[index] = DynamicValueHelper.Deserialize(
                actionRequest.Parameters[realParameterIndex],
                endpointParameter.ParameterType
            );

            realParameterIndex++;
        }

        context.ConstructedParameters = constructedParameters;

        return Task.CompletedTask;
    }
}
