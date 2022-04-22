using Kantaiko.Routing.Context;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.Context;

public static class ContextExtensions
{
    public static IStateKeyFactory? GetStateKeyFactory(this IContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return StateContextProperties.Of(context)?.StateKeyFactory;
    }
}
