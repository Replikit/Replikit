namespace Replikit.Extensions.Views.Models;

public record ViewInstanceAction(string Method, IReadOnlyList<object> Parameters);
