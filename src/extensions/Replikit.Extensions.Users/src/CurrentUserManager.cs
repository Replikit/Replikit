using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Routing.Context;

namespace Replikit.Extensions.Users;

internal class CurrentUserManager<TUser, TUserId> : ICurrentUserManager<TUser, TUserId>
    where TUser : ReplikitUser<TUserId>, new()
{
    private readonly UserManager<TUser, TUserId> _userManager;
    private readonly IAdapterEventContextAccessor _contextAccessor;

    public CurrentUserManager(UserManager<TUser, TUserId> userManager,
        IAdapterEventContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    public Task<TUser?> GetCurrentUserOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        if (_contextAccessor.CurrentContext is not IAdapterEventContext<IAccountEvent> context)
        {
            throw new ReplikitException("Current user cannot be retrieved outside of an account event context.");
        }

        return _userManager.FindByAccountIdAsync(context.Event.Account.Id, cancellationToken);
    }

    public Task<TUser> EnsureCurrentUserCreatedAsync(CancellationToken cancellationToken = default)
    {
        if (_contextAccessor.CurrentContext is not IAdapterEventContext<IAccountEvent> context)
        {
            throw new ReplikitException("Current user cannot be retrieved outside of an account event context.");
        }

        return _userManager.EnsureUserCreatedAsync(context.Event.Account.Id, cancellationToken);
    }
}
