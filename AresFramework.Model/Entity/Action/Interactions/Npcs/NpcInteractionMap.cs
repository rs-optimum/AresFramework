using System.Runtime.CompilerServices;
using NLog;

namespace AresFramework.Model.Entity.Action.Interactions.Npcs;


public class NpcInteractionMap
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private readonly int _npcId;
        
    public NpcInteractionMap(int npcId)
    {
        _npcId = npcId;
    }

    /// <summary>
    /// A list of all the interactions
    /// </summary>
    private readonly Dictionary<string, NpcInteractionAction.HandleInteraction> _mappedInteractions = new();

    /// <summary>
    /// Adds an interaction to the _mappedInteractions
    /// </summary>
    /// <param name="option">The option to add</param>
    /// <param name="interaction">the interactions to add</param>
    public void AddInteraction(string option, NpcInteractionAction.HandleInteraction interaction, string source = "")
    {
        _mappedInteractions.TryGetValue(option, out var foundValue);
        if (foundValue == null)
        {
            _mappedInteractions.Add(option, interaction);
        }
        else
        {
            Log.Warn($"The '{option}' interaction has been overriden for the npc id {_npcId} by the following class: {source}");
            _mappedInteractions[option] = foundValue;
        }
    }
    
    /// <summary>
    /// A default interaction when there's no map
    /// </summary>
    private static readonly NpcInteractionAction.HandleInteraction DefaultInteraction = (player, npc, option) =>
    {
        player.SendMessage("Nothing interesting happens.");
    };
    
    /// <summary>
    /// Gets an interaction map if possible, or return <see cref="DefaultInteraction"/>
    /// </summary>
    /// <param name="option">the option we are calling - should be lowercase</param>
    /// <returns></returns>
    public NpcInteractionAction.HandleInteraction GetInteraction(string option)
    {
        _mappedInteractions.TryGetValue(option, out var foundValue);
        return foundValue ?? DefaultInteraction;
    }
}