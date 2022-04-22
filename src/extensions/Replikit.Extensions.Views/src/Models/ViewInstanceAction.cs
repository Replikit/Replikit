using Replikit.Extensions.Storage.Models;

namespace Replikit.Extensions.Views.Models;

public record ViewInstanceAction(string Method, IReadOnlyList<DynamicValue> Parameters);
