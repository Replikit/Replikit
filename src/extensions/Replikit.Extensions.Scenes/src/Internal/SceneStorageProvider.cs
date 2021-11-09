using Microsoft.Extensions.Options;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Options;
using Replikit.Extensions.Common.Scenes;

namespace Replikit.Extensions.Scenes.Internal;

internal class SceneStorageProvider : StorageProvider<ISceneStorage>, ISceneStorageProvider
{
    public SceneStorageProvider(IServiceProvider serviceProvider,
        IOptions<StorageProviderOptions<ISceneStorage>> options) : base(serviceProvider, options) { }
}
