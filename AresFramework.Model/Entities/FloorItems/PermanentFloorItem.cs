using AresFramework.Model.Entities.Players;
using AresFramework.Model.Items;
using AresFramework.Model.World;
using NLog;

namespace AresFramework.Model.Entities.FloorItems;

public class PermanentFloorItem : FloorItem
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public PermanentFloorItem(Item item, Position position, int lifeTime = FloorItemConstants.FloorItemLifetime, Player? owner = null) 
        : base(item, position, lifeTime, null)
    {
    }

    public override Item? OnPickup(Player player)
    {
        if (player.Inventory.HasSpaceFor(_item))
        {
            var transaction = player.Inventory.Transaction.AddItem(_item);
            Log.Info($"added {_item} {player.Username}");
            Destroyed = true;
            return _item;
        }
        player.SendMessage("You don't have enough space in your inventory for that item!");
        return _item;
    }
}