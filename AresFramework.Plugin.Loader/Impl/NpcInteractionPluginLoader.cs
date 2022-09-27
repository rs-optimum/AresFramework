using AresFramework.Model.Plugins.Entities.Npcs.Interactions;
using JetBrains.Annotations;
using NLog;

namespace AresFramework.Plugin.Loaders.Impl;

[UsedImplicitly]
public class NpcInteractionPluginLoader : IPluginLoader
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public void LoadPlugins()
    {
        PluginLoader.InitializeTypes<NpcInteractionsPlugin>(typeof(NpcInteractionsPlugin), (type, name, assembly) =>
        {
            type.BuilderAssembly = assembly;
            type.BuildInteractions();
        });
    }
}