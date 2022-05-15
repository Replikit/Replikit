using Replikit.Core.Common;

namespace Replikit.Extensions.Views.Models;

public record ViewInstanceAction(string Method, IReadOnlyList<DynamicValue> Parameters);
