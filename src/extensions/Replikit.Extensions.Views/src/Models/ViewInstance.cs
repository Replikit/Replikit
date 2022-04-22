namespace Replikit.Extensions.Views.Models;

public record ViewInstance(string Type, IReadOnlyList<ViewInstanceAction> Actions);
