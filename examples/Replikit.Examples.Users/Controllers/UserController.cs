using System.Text;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Core.GlobalServices;
using Replikit.Extensions.Users;
using Replikit.Extensions.Users.Parameters;

namespace Replikit.Examples.Users.Controllers;

internal class UserController : Controller
{
    private readonly IGlobalAdapterRepository _adapterRepository;
    private readonly UserManager<ReplikitUser, Guid> _userManager;

    public UserController(IGlobalAdapterRepository adapterRepository, UserManager<ReplikitUser, Guid> userManager)
    {
        _adapterRepository = adapterRepository;
        _userManager = userManager;
    }

    [Command("profile")]
    public async Task<OutMessage> GetUserInfo([CurrentUser] ReplikitUser user)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"User Id: {user.Id}");
        builder.AppendLine($"Username: {user.Username ?? "Not set"}");

        builder.AppendLine("Accounts:");

        foreach (var accountId in user.AccountIds)
        {
            var accountInfo = await _adapterRepository.GetAccountInfoAsync(accountId);

            var displayName = accountInfo switch
            {
                { FirstName: { } firstName, LastName: { } lastName } => $"{firstName} {lastName}",
                { FirstName: { } firstName } => firstName,
                { Username: { } username } => username,
                _ => "Unknown"
            };

            builder.Append($"[{accountId.AdapterId.Type}] [{accountId.Value}] {displayName}");
        }

        return OutMessage.FromCode(builder.ToString());
    }

    [Command("username")]
    public async Task<OutMessage> ChangeUsername([CurrentUser] ReplikitUser user, string username)
    {
        var success = await _userManager.ChangeUsernameAsync(user, username);

        return OutMessage.FromCode(success ? "Username changed" : "Username already taken");
    }
}
