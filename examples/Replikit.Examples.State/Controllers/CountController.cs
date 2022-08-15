using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Examples.State.States;
using Replikit.Extensions.State;

namespace Replikit.Examples.State.Controllers;

public class CountController : Controller
{
    private readonly IGlobalState<TestState> _globalState;
    private readonly IChannelState<TestState> _channelState;
    private readonly IAccountState<TestState> _accountState;

    public CountController(IGlobalState<TestState> globalState, IChannelState<TestState> channelState,
        IAccountState<TestState> accountState)
    {
        _globalState = globalState;
        _channelState = channelState;
        _accountState = accountState;
    }

    [Command("count")]
    public OutMessage Count()
    {
        _globalState.Value.Count++;
        _channelState.Value.Count++;
        _accountState.Value.Count++;

        return new OutMessage
        {
            Text =
            {
                TextToken.Line($"Global count: {_globalState.Value.Count}"),
                TextToken.Line($"Channel count: {_channelState.Value.Count}"),
                TextToken.Line($"Account count: {_accountState.Value.Count}")
            }
        };
    }
}
