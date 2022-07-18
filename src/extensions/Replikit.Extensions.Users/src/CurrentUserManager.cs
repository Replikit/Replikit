using Kantaiko.Routing.Context;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.Abstractions.Users;
using Replikit.Core.Handlers.Context;
using Replikit.Core.Services;

namespace Replikit.Extensions.Users;

internal class CurrentUserManager<TUser, TUserId> : ContextService<IAccountEvent>, ICurrentUserManager<TUser, TUserId>
    where TUser : ReplikitUser<TUserId>, new()
{
    private readonly UserManager<TUser, TUserId> _userManager;

    public CurrentUserManager(ContextAccessor<IAdapterEventContext<IAccountEvent>> contextAccessor,
        UserManager<TUser, TUserId> userManager) : base(contextAccessor)
    {
        _userManager = userManager;
    }

    public Task<TUser?> GetCurrentUserOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        return _userManager.FindByAccountIdAsync(Event.Account.Id, cancellationToken);
    }

    public Task<TUser> EnsureCurrentUserCreatedAsync(CancellationToken cancellationToken = default)
    {
        return _userManager.EnsureUserCreatedAsync(Event.Account.Id, cancellationToken);
    }
}
