namespace Replikit.Extensions.Views.Internal;

internal interface IStatefulView
{
    Type StateType { get; }
    void SetState(object state);
    object StateValue { get; }
}
