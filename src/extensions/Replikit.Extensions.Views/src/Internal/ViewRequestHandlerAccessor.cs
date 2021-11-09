using Kantaiko.Controllers;
using Kantaiko.Controllers.Introspection;

namespace Replikit.Extensions.Views.Internal;

internal class ViewRequestHandlerAccessor
{
    public RequestHandler<ViewContext> RequestHandler { get; set; } = null!;
    public RequestHandlerInfo Info => RequestHandler.Info;
}
