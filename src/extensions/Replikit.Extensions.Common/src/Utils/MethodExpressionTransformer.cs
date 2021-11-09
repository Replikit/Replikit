using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Replikit.Extensions.Common.Utils;

public static class MethodExpressionTransformer
{
    public static (MethodInfo, object[]) Transform(Expression expression)
    {
        if (expression is not LambdaExpression lambdaExpression)
        {
            throw new InvalidExpressionException("Action expression must be a lambda expression");
        }

        if (lambdaExpression.Body is not MethodCallExpression methodCall)
        {
            throw new InvalidExpressionException("Action expression must be a method call");
        }

        var parameters = methodCall.Arguments
            .Select(x => Expression.Lambda(x).Compile().DynamicInvoke()!)
            .ToArray();

        return (methodCall.Method, parameters);
    }
}
