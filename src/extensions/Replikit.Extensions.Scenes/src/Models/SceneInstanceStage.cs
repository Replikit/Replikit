using Replikit.Core.Common;

namespace Replikit.Extensions.Scenes.Models;

public record SceneInstanceStage(string Type, string Method, IReadOnlyList<DynamicValue> Parameters);
