using AresFramework.Model.Entities.Players;
using AresFramework.Model.Plugins.Items;
using NLog;

namespace AresFramework.Model.Handlers;

public static class DropItemHandler
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// A list of all mapped item actions
    /// </summary>
    public static readonly Dictionary<int, IItemDropProcessor> ItemActions = new Dictionary<int, IItemDropProcessor>();
    
    /// <summary>
    /// Drops items to the ground or destroys them
    /// </summary>
    /// <param name="player"></param>
    /// <param name="slot"></param>
    public static void DropItem(Player player, int slot)
    {
        var item = player.Inventory.Get(slot);
        if (item == null)
        {
            return;
        }

        ItemActions.TryGetValue(item.Id, out var action);
        if (action != null)
        {
            // Drop item here;
            Log.Info("Drop custom");
            action.DropItem(player, item, slot);
        }
        
        // TODO: drop item into world
    }

}