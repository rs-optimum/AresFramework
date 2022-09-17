using AresFramework.Model.Entity.Action;
using AresFramework.Model.Entity.Action.Interactions.Npcs;
using NLog;

namespace AresFramework.Plugin.Loaders.Impl;

public class NpcInteractionPluginLoader : IPluginLoader
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public void LoadPlugins()
    {
        PluginLoader.InitializeTypes<NpcInteractionAction>(typeof(NpcInteractionAction), (type, name, assembly) =>
        {
            type.BuilderAssembly = assembly;
            type.BuildInteractions();
        });
    }
}