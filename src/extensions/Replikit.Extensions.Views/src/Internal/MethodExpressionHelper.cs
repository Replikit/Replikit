using System.Linq.Expressions;
using System.Reflection;
using Replikit.Extensions.Views.Actions;

namespace Replikit.Extensions.Views.Internal;

internal static class MethodExpressionHelper
{
    public readonly record struct MethodExpressionInfo(MethodInfo Method, object[] Parameters);

    public static MethodExpressionInfo ExtractInfo(Expression expression)
    {
        if (expression is not LambdaExpression lambdaExpression)
        {
            throw new InvalidOperationException("Action expression must be a lambda expression");
        }

        if (lambdaExpression.Body is not MethodCallExpression methodCall)
        {
            throw new InvalidOperationException("Action expression must be a method call");
        }

        var parameters = methodCall.Arguments
            .Where(x => x.Type != typeof(IViewActionContext))
            .Select(x => Expression.Lambda(x).Compile().DynamicInvoke()!)
            .ToArray();

        return new MethodExpressionInfo(methodCall.Method, parameters);
    }
}
