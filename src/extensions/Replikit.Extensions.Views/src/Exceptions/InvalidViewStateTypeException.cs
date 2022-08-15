using Replikit.Core.Abstractions.State;

namespace Replikit.Extensions.Views.Exceptions;

public class InvalidViewStateTypeException : Exception
{
    internal InvalidViewStateTypeException(StateKind stateKind) : base(
        $"{stateKind} state is not supported inside view. Only channel states are supported.") { }
}
