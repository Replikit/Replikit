namespace Replikit.Extensions.Scenes.Internal;

internal interface IStatefulScene
{
    Type StateType { get; }
    void SetState(object state);
    object StateValue { get; }
}
