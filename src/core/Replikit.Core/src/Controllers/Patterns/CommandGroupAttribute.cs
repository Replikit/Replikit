using Kantaiko.Controllers.Design.Controllers;
using Kantaiko.Controllers.Design.Properties;

namespace Replikit.Core.Controllers.Patterns;

[AttributeUsage(AttributeTargets.Class)]
public class CommandGroupAttribute : Attribute, IControllerDesignPropertyProvider
{
    private readonly string _groupName;

    public CommandGroupAttribute(string groupName)
    {
        _groupName = groupName;
    }

    public DesignPropertyCollection GetControllerDesignProperties() => new()
    {
        [ReplikitControllerProperties.CommandGroupName] = _groupName
    };
}
