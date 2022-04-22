using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Management.Services;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace Replikit.Examples.AdapterServices.Controllers;

public class MemberController : Controller
{
    [Command("count members")]
    public async Task<OutMessage> CountMembers()
    {
        if (!Adapter.MemberService.Supports(MemberServiceFeatures.GetTotalCount))
        {
            return "Current platform does not support member counting";
        }

        var count = await Adapter.MemberService.GetTotalCountAsync(Channel.Id);
        return $"Count: {count}";
    }
}
