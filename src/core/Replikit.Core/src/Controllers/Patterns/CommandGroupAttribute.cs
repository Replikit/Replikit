using Kantaiko.Controllers.Introspection.Factory.Attributes;
using Kantaiko.Controllers.Introspection.Factory.Context;
using Kantaiko.Properties.Immutable;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Class)]
public class CommandGroupAttribute : Attribute, IControllerPropertyProvider
{
    private readonly string _groupName;

    public CommandGroupAttribute(string groupName)
    {
        _groupName = groupName;
    }

    public IImmutablePropertyCollection UpdateControllerProperties(ControllerFactoryContext context)
    {
        return context.Controller.Properties.Set(new CommandGroupControllerProperties(_groupName));
    }
}
