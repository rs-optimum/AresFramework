using AresFramework.Model.Handlers;
using AresFramework.Model.Plugins.Items;
using JetBrains.Annotations;
using NLog;

namespace AresFramework.Plugin.Loaders.Impl;

/// <summary>
/// Item Actions Loader
/// </summary>
[UsedImplicitly]
public class ItemActionsLoader : IPluginLoader
{
    
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public void LoadPlugins()
    {
        PluginLoader.InitializeTypes<IItemDropProcessor>(typeof(IItemDropProcessor), (type, name, assembly) =>
        {
            foreach (var ids in type.AttachedIds())
            {
                DropItemHandler.ItemActions[ids] = type;
            }
        });
    }
}