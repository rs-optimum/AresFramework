using AresFramework.Model.Entities.Players;
using AresFramework.Model.Items;

namespace AresFramework.Model.Plugins.Items;

/// <summary>
/// This defines how an item will drop
/// </summary>
public interface IItemDropProcessor : IAttachableIds
{

    public void DropItem(Player player, Item item, int slot)
    {
        
    }
    
}