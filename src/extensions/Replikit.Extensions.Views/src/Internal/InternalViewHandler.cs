using Kantaiko.Controllers.Execution;
using Kantaiko.Controllers.Introspection;

namespace Replikit.Extensions.Views.Internal;

internal readonly record struct InternalViewHandler(ControllerInfo ControllerInfo,
    IControllerHandler<InternalViewContext> Handler);
