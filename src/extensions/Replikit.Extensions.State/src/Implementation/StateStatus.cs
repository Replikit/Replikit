namespace Replikit.Extensions.State.Implementation;

internal enum StateStatus : byte
{
    Empty,
    HasValue,
    HasModifiedValue,
    HasClearedValue
}
