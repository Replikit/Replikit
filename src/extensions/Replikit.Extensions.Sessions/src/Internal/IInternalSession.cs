using Replikit.Extensions.Common.Sessions;

namespace Replikit.Extensions.Sessions.Internal;

internal interface IInternalSession
{
    Type ValueType { get; }
    object Value { get; set; }

    SessionKey SessionKey { get; set; }

    Task<SessionKey> CreateSessionKeyAsync();
}
