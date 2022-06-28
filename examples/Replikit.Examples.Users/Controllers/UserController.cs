using System.Text;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Controllers.Patterns;
using Replikit.Core.GlobalServices;
using Replikit.Extensions.Users;
using Replikit.Extensions.Users.Controllers;
using Replikit.Extensions.Users.Parameters;

namespace Replikit.Examples.Users.Controllers;

internal class UserController : UserController<ReplikitUser, Guid>
{
    private readonly IGlobalAdapterRepository _adapterRepository;

    public UserController(UserManager<ReplikitUser, Guid> userManager, IGlobalAdapterRepository adapterRepository) :
        base(userManager)
    {
        _adapterRepository = adapterRepository;
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
            var accountInfo = await _adapterRepository.GetAccountInfoAsync(accountId, CancellationToken);

            var displayName = accountInfo switch
            {
                { FirstName: { } firstName, LastName: { } lastName } => $"{firstName} {lastName}",
                { FirstName: { } firstName } => firstName,
                { Username: { } username } => username,
                _ => "Unknown"
            };

            builder.Append($"[{accountId.AdapterId.Type}] [{accountId.Identifier}] {displayName}");
        }

        return OutMessage.FromCode(builder.ToString());
    }
}
