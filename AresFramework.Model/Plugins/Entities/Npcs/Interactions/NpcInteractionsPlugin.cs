using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using AresFramework.Model.Entities.Npcs;
using AresFramework.Model.Entities.Npcs.Action;
using AresFramework.Model.Entities.Players;
using AresFramework.Plugin.Ignore;
using NLog;

namespace AresFramework.Model.Plugins.Entities.Npcs.Interactions;

public abstract class NpcInteractionsPlugin
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public Assembly? BuilderAssembly { get; set; }

    /// <summary>
    /// Builds all the possible interactions for the given <see cref="AttachedNpcs"/>
    /// </summary>
    public abstract void BuildInteractions();
    
    /// <summary>
    /// All of the Npc's attached to this interaction
    /// </summary>
    /// <returns></returns>
    public abstract int[] AttachedNpcs();
    
    /// <summary>
    /// Handles the action itself
    /// </summary>
    public delegate void HandleInteraction(Player player, Npc npc, NpcMapOption? option = null);

    
    /// <summary>
    /// Maps a interaction to an option for the given <see cref="AttachedNpcs"/>
    /// </summary>
    /// <param name="option">the option we are adding</param>
    /// <param name="handler"></param>
    protected void Map(string option, HandleInteraction handler, [CallerFilePath] string file = "")
    {
        var mapped = new List<int>();
        foreach (var npcId in AttachedNpcs())
        {
            var currentMap = ClickNpcHandler.GetInteractionMap(npcId);

            // This will be used for checking ignores
            if (ShouldIgnoreAdd(npcId, option))
            {
                continue;
            }
            
            currentMap.AddInteraction(option, handler, file);
            mapped.Add(npcId);
        }
        Log.Debug($"Mapped the npc id's {string.Join(", ", mapped)} to the following option: '{option}'");
    }

    private bool ShouldIgnoreAdd(int id, string option)
    {
        if (BuilderAssembly != null)
        {
            PluginIgnores.MappedPluginData.TryGetValue(BuilderAssembly.GetName().FullName, out var values);
            if (values != null && values.IsNpcInteractionIgnored(id, option))
            {
                Log.Debug($"Stopped mapping npcid {id} to '{option}' because {BuilderAssembly.GetName().Name} plugin folder contains ignores.json");
                return true;
            }
        }

        return false;
    }

    

}