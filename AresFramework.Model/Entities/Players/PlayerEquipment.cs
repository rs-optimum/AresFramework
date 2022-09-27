using AresFramework.Model.Items;
using AresFramework.Model.Items.Containers;

namespace AresFramework.Model.Entities.Players;

/// <summary>
/// The players equipment
/// </summary>
public sealed class PlayerEquipment
{
    
    public const int Head = 0;
    public const int Cape = 1;
    public const int Amulet = 2;
    public const int Weapon = 3;
    public const int Plate = 4;
    public const int Shield = 5;
    public const int Legs = 7;
    public const int Hands = 9;
    public const int Feet = 10;
    public const int Ring = 12;
    public const int Ammunition = 13;
    
    private ItemContainer EquipmentContainer { get; set; } =
        new ItemContainer(14, ItemContainer.ItemStackPolicy.Default, ItemContainer.ItemMovePolicy.Swap);


    /// <summary>
    /// Sets the equipment
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="item"></param>
    public void Set(int slot, Item item)
    {
        EquipmentContainer.Set(slot, item);
    }
    
}