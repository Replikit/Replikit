using Microsoft.Extensions.Options;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Options;
using Replikit.Extensions.Common.Sessions;
using Replikit.Extensions.Sessions.Options;

namespace Replikit.Extensions.Sessions.Internal;

internal class SessionStorageProvider : StorageProvider<ISessionStorage>, ISessionStorageProvider
{
    public SessionStorageProvider(IServiceProvider serviceProvider,
        IOptions<StorageProviderOptions<ISessionStorage>> options) : base(serviceProvider, options) { }
}
