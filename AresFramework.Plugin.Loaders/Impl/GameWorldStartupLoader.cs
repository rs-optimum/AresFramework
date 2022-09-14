using AresFramework.Plugin.Module.World;

namespace AresFramework.Plugin.Loaders.Impl;

public class GameWorldStartupLoader : IPluginLoader
{
    public void LoadPlugins()
    {
        PluginLoader.InitializeTypes<IGameWorldInitializer>(typeof(IGameWorldInitializer), (type, name) =>
        {
            type.LoadGameWorld();
        });
    }
}