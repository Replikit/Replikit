using Microsoft.Extensions.Options;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Options;
using Replikit.Extensions.Common.Views;

namespace Replikit.Extensions.Views.Internal;

internal class ViewStorageProvider : StorageProvider<IViewStorage>, IViewStorageProvider
{
    public ViewStorageProvider(IServiceProvider serviceProvider, IOptions<StorageProviderOptions<IViewStorage>> options)
        : base(serviceProvider, options) { }
}
