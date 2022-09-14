using AresFramework.Model.Entity.Action.Interactions.Npcs;

namespace AresFramework.Model.Entity.Action;

public class ClickNpcHandler
{
    
    public static void ClickNpc(Player player, Npc npc, string option)
    {
        var interactionMap = NpcInteractionAction.Get(npc.Id);
        if (interactionMap == null)
        {
            player.SendMessage("Nothing interesting happens.");
            return;
        }
        
        var interaction = interactionMap.GetInteraction(option);
        interaction.Invoke(player, npc);

    }
}