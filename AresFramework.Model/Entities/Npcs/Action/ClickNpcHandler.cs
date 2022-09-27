using System.Collections.Concurrent;
using AresFramework.Model.Entities.Players;
using AresFramework.Model.Plugins.Entities.Npcs.Interactions;
using NLog;

namespace AresFramework.Model.Entities.Npcs.Action;

/// <summary>
/// Handles the clicking of npcs
/// </summary>
public static class ClickNpcHandler
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// A list of all the npc interaction maps
    /// </summary>
    private static readonly ConcurrentDictionary<int, NpcInteractionMap> InteractionMaps = new ConcurrentDictionary<int, NpcInteractionMap>();
    
    public static void ClickNpc(Player player, Npc npc, string option)
    {
        Log.Debug($"{player.Username} clicked {npc} with option: {option}");
        var interactionMap = Get(npc.Id);
        if (interactionMap == null)
        {
            player.SendMessage("Nothing interesting happens.");
            return;
        }
        
        var interaction = interactionMap.GetInteraction(option);
        interaction.Invoke(player, npc);
    }
    
    
    /// <summary>
    /// Returns a guaranteed <see cref="NpcInteractionMap"/>
    /// </summary>
    /// <param name="id">The id of the npc to return a <see cref="NpcInteractionMap"/></param>
    /// <returns></returns>
    public static NpcInteractionMap GetInteractionMap(int id)
    {
        InteractionMaps.TryGetValue(id, out var foundValue);
        if (foundValue != null)
        {
            return foundValue;
        }
        var newInteractionMap = new NpcInteractionMap(id);
        
        InteractionMaps[id] = newInteractionMap;
        return newInteractionMap;
    }

    public static NpcInteractionMap? Get(int id)
    {
        InteractionMaps.TryGetValue(id, out var foundValue);
        return foundValue;
    }
    
}