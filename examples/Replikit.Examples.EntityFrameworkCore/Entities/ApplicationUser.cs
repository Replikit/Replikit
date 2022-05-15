using Replikit.Core.Abstractions.Users;

namespace Replikit.Examples.EntityFrameworkCore.Entities;

internal class ApplicationUser : ReplikitUser<long>
{
    public int CustomProperty { get; set; }
}
