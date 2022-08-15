using System.Reflection;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Kantaiko.Controllers.Result;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views.ExecutionHandlers;

internal class InvokeOptionalEndpointHandler : IControllerExecutionHandler<InternalViewContext>
{
    public Task HandleAsync(ControllerExecutionContext<InternalViewContext> context)
    {
        if (context.Endpoint is null)
        {
            return Task.CompletedTask;
        }

        try
        {
            context.RawInvocationResult = context.Endpoint.MethodInfo
                .Invoke(context.ControllerInstance, context.ConstructedParameters);
        }
        catch (TargetInvocationException exception)
        {
            context.ExecutionResult = ControllerExecutionResult.Exception(exception.InnerException!);
        }

        return Task.CompletedTask;
    }
}
