using Kantaiko.Controllers.ParameterConversion;
using Kantaiko.Controllers.ParameterConversion.Text;
using Replikit.Core.Abstractions.Users;

namespace Replikit.Extensions.Users.Parameters;

internal class CurrentUserConverter<TUser, TUserId> : AsyncTextParameterConverter<TUser>
    where TUser : ReplikitUser<TUserId>, new()
{
    private readonly ICurrentUserManager<TUser, TUserId> _userManager;

    public CurrentUserConverter(ICurrentUserManager<TUser, TUserId> userManager)
    {
        _userManager = userManager;
    }

    public override bool CheckValueExistence(TextParameterConversionContext context)
    {
        return true;
    }

    public override async Task<ResolutionResult<TUser>> ResolveAsync(TextParameterConversionContext context,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.EnsureCurrentUserCreatedAsync(cancellationToken);

        return ResolutionResult.Success(user);
    }
}
