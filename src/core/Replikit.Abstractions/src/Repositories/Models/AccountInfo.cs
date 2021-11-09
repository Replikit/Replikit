using System.Globalization;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Repositories.Models;

public sealed record AccountInfo(
    GlobalIdentifier Id,
    string? Username = null,
    string? FirstName = null,
    string? LastName = null,
    PhotoAttachment? Avatar = null,
    CultureInfo? CultureInfo = null
);
