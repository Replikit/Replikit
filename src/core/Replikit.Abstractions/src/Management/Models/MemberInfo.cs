using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Management.Features;
using Replikit.Abstractions.Repositories.Models;

namespace Replikit.Abstractions.Management.Models;

public record MemberInfo(Identifier ParentId, AccountInfo AccountInfo,
    MemberPermissions Permissions = MemberPermissions.None);
