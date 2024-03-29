using Replikit.Abstractions.Accounts.Events;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Routing.Context;

namespace Replikit.Extensions.Users;

internal class CurrentUserManager<TUser, TUserId> : ICurrentUserManager<TUser, TUserId>
    where TUser : ReplikitUser<TUserId>, new()
{
    private readonly UserManager<TUser, TUserId> _userManager;
    private readonly IBotEventContextAccessor _contextAccessor;

    public CurrentUserManager(UserManager<TUser, TUserId> userManager,
        IBotEventContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public Task<TUser?> GetCurrentUserOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        if (_contextAccessor.CurrentContext is not IBotEventContext<IAccountEvent> context)
        {
            throw new ReplikitException("Current user cannot be retrieved outside of an account event context.");
        }

        return _userManager.FindByAccountIdAsync(context.Event.Account.Id, cancellationToken);
    }

    public Task<TUser> EnsureCurrentUserCreatedAsync(CancellationToken cancellationToken = default)
    {
        if (_contextAccessor.CurrentContext is not IBotEventContext<IAccountEvent> context)
        {
            throw new ReplikitException("Current user cannot be retrieved outside of an account event context.");
        }

        return _userManager.EnsureUserCreatedAsync(context.Event.Account.Id, cancellationToken);
    }
}
