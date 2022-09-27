using AresFramework.Plugin.Module.World;
using JetBrains.Annotations;

namespace AresFramework.Plugin.Loaders.Impl;

[UsedImplicitly]
public class GameWorldStartupLoader : IPluginLoader
{
    public void LoadPlugins()
    {
        PluginLoader.InitializeTypes<IGameWorldInitializer>(typeof(IGameWorldInitializer), (type, name, assembly) =>
        {
            type.LoadGameWorld();
        });
    }
}