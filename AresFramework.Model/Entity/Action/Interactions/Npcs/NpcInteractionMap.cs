using NLog;

namespace AresFramework.Model.Entity.Action.Interactions.Npcs;


public class NpcInteractionMap
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public readonly int Id;
        
    public NpcInteractionMap(int id)
    {
        Id = id;
    }

    /// <summary>
    /// A list of all the interactions
    /// </summary>
    private readonly Dictionary<string, NpcInteractionAction.HandleInteraction> _mappedInteractions = new Dictionary<string, NpcInteractionAction.HandleInteraction>();

    /// <summary>
    /// Adds an interaction to the _mappedInteractions
    /// </summary>
    /// <param name="option">The option to add</param>
    /// <param name="interaction">the interactions to add</param>
    public void AddInteraction(string option, NpcInteractionAction.HandleInteraction interaction)
    {
        _mappedInteractions.TryGetValue(option, out var foundValue);
        if (foundValue == null)
        {
            _mappedInteractions.Add(option, interaction);
        }
        else
        {
            Log.Warn($"The following '{option}' interaction has been overriden for the npc id {Id}");
            _mappedInteractions[option] = foundValue;
        }
    }

    private static readonly NpcInteractionAction.HandleInteraction DefaultInteraction = (player, npc, option) =>
    {
        player.SendMessage("Nothing interesting happens.");
    };
    
    /// <summary>
    /// Gets an interaction map if possible, or return <see cref="DefaultInteraction"/>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public NpcInteractionAction.HandleInteraction GetInteraction(string option)
    {
        _mappedInteractions.TryGetValue(option, out var foundValue);
        if (foundValue != null)
        {
            return foundValue;
        }
        
        return DefaultInteraction;
    }
}