using Kantaiko.Controllers.Introspection;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Extensions.Views.Exceptions;

public class ViewInstanceNotFoundException : ReplikitException
{
    public ViewInstanceNotFoundException(ControllerInfo controllerInfo, MessageIdentifier viewId) : base(
        $"View of type \"{controllerInfo.Type.Name}\" with id \"{viewId}\" was not found") { }
}
