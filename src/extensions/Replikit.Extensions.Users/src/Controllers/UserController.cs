using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Extensions.Users.Parameters;
using Replikit.Extensions.Users.Resources;

namespace Replikit.Extensions.Users.Controllers;

public class UserController<TUser, TUserId> : Controller where TUser : ReplikitUser<TUserId>, new()
{
    private readonly UserManager<TUser, TUserId> _userManager;

    public UserController(UserManager<TUser, TUserId> userManager)
    {
        _userManager = userManager;
    }

    [Command("username")]
    public virtual async Task<OutMessage> ChangeUsername([CurrentUser] TUser user, string username)
    {
        var result = await _userManager.ChangeUsernameAsync(user, username, CancellationToken);

        return OutMessage.FromCode(result ? Locale.UsernameSuccessfullyChanged : Locale.UsernameTaken);
    }
}
