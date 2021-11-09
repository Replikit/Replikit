using Kantaiko.Hosting.Modules;

namespace Replikit.Core.Modules;

internal class ReplikitModuleCollection : IReplikitModuleCollection
{
    private readonly IModuleCollection _moduleCollection;

    public ReplikitModuleCollection(IModuleCollection moduleCollection)
    {
        _moduleCollection = moduleCollection;
    }

    public void Add<T>() where T : class, IModule
    {
        _moduleCollection.Add<T>();
    }
}
