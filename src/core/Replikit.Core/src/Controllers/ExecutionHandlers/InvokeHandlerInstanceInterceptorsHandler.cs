using Kantaiko.Controllers.Exceptions;
using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Execution.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Routing;

namespace Replikit.Core.Controllers.ExecutionHandlers;

public class InvokeHandlerInstanceInterceptorsHandler<TContext> : IControllerExecutionHandler<TContext>
{
    public async Task HandleAsync(ControllerExecutionContext<TContext> context)
    {
        PropertyNullException.ThrowIfNull(context.ControllerInstance);

        var instanceInterceptors = context.ServiceProvider
            .GetRequiredService<IEnumerable<IHandlerInstanceInterceptor>>();

        foreach (var instanceInterceptor in instanceInterceptors)
        {
            await instanceInterceptor.InterceptAsync(context.ControllerInstance, context.CancellationToken);
        }
    }
}
