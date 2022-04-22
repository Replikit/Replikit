using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Services;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Management.Models;

public record MemberInfo(Identifier ParentId, AccountInfo AccountInfo,
    MemberPermissions Permissions = MemberPermissions.None);
