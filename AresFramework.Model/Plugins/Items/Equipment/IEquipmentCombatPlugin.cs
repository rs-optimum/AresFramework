using AresFramework.Model.Entities;
using AresFramework.Model.Entities.Players;
using AresFramework.Model.Items;

namespace AresFramework.Model.Plugins.Items.Equipment;

/// <summary>
/// Represents combat equipment
/// </summary>
public interface IEquipmentCombatPlugin : IAttachableIds
{
    
    public void OnAttack(Player attacker, Entity defender, Item item, int slot)
    {
        
    }
    
    /// <summary>
    /// What happens when the player is attacked by another entity, regardless if they're hit
    /// successfully or not
    /// </summary>
    /// <param name="player">The player being defended</param>
    /// <param name="attacker">The entity whom is attacking the player</param>
    /// <param name="item">The item part of the defence</param>
    /// <param name="slot">the item slot</param>
    public void OnDefence(Player player, Entity attacker, Item item, int slot)
    {
        
    }

    public void OnSuccessfulAttack(Player player, Entity attacker, Item item, int slot)
    {
        
    }
    
    /// <summary>
    /// On successful defence is where the person hit's 0
    /// </summary>
    /// <param name="player"></param>
    /// <param name="item"></param>
    /// <param name="slot"></param>
    public void OnSuccessfulDefence(Player player, Item item, int slot)
    {
        
    }


}