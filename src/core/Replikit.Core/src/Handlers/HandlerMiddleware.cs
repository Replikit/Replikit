using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Routing;
using Replikit.Core.Routing.Context;
using Replikit.Core.Routing.Middleware;

namespace Replikit.Core.Handlers;

internal class HandlerMiddleware : IBotEventMiddleware
{
    private readonly IReadOnlyList<Type> _handlerTypes;

    public HandlerMiddleware(Assembly assembly)
    {
        _handlerTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(BotEventHandler)))
            .ToArray();
    }

    public async Task HandleAsync(IBotEventContext context, BotEventDelegate next)
    {
        var instanceInterceptors = context.ServiceProvider
            .GetRequiredService<IEnumerable<IHandlerInstanceInterceptor>>();

        foreach (var handlerType in _handlerTypes)
        {
            var handler = (BotEventHandler) ServiceHandlerFactory.Instance
                .CreateHandler(handlerType, context.ServiceProvider);

            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var instanceInterceptor in instanceInterceptors)
            {
                await instanceInterceptor.InterceptAsync(handler, context.CancellationToken);
            }

            await handler.HandleAsync(context);

            if (context.IsHandled)
            {
                return;
            }
        }

        await next(context);
    }
}
