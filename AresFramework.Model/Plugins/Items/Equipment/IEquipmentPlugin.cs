using AresFramework.Model.Entities.Players;

namespace AresFramework.Model.Plugins.Items.Equipment;

/// <summary>
/// Represents what happens when equipment is worn
/// </summary>
public interface IEquipmentPlugin : IAttachableIds
{
    /// <summary>
    /// What happens when someone wears the equipment
    /// </summary>
    /// <param name="player"></param>
    /// <param name="id"></param>
    /// <param name="slot"></param>
    public void OnAdd(Player player, int id, int slot)
    {
        
    }

    /// <summary>
    /// What happens when someone removes the equipment
    /// </summary>
    /// <param name="player"></param>
    /// <param name="dropped"></param>
    /// <param name="slot"></param>
    public void OnRemove(Player player, int dropped, int slot)
    {
        
    }
}