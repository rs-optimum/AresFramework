using NLog;

namespace AresFramework.Model.Entity.Action.Interactions.Npcs;

/// <summary>
/// Defines what an NpcInteraction is
/// </summary>
public abstract class NpcInteractionAction
{
    
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// A list of all the npc interaction maps
    /// </summary>
    private static readonly Dictionary<int, NpcInteractionMap> InteractionMaps = new Dictionary<int, NpcInteractionMap>();
    
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
    protected void Map(string option, HandleInteraction handler)
    {
        foreach (var npcId in AttachedNpcs())
        {
            var currentMap = GetInteractionMap(npcId);
            currentMap.AddInteraction(option, handler);
        }
        Log.Debug($"Mapped the following npc id's {string.Join(", ", AttachedNpcs())} to the following option: '{option}'");
    }

    /// <summary>
    /// Returns a guaranteed <see cref="NpcInteractionMap"/>
    /// </summary>
    /// <param name="id">The id of the npc to return a <see cref="NpcInteractionMap"/></param>
    /// <returns></returns>
    private static NpcInteractionMap GetInteractionMap(int id)
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