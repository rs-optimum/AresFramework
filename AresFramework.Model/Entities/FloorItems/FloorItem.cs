using AresFramework.Model.Entities.FloorItems.Listeners;
using AresFramework.Model.Entities.Players;
using AresFramework.Model.Items;
using AresFramework.Model.World;
using NLog;

namespace AresFramework.Model.Entities.FloorItems;

/// <summary>
/// Represents an item on the floor
/// </summary>
public class FloorItem : Entity
{
    public static readonly IFloorItemListener Default = new GlobalFloorItemListener();
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    protected readonly Item _item;
    private int _lifeTime;
    private int ticksAlive = 0;
    private Player? _owner;
    private IFloorItemListener _listener;

    public FloorItem(
        Item item, 
        Position position, 
        int lifeTime = FloorItemConstants.FloorItemLifetime, 
        Player? owner = null,
        IFloorItemListener? listener = null) : base(position)
    {
        _item = item;
        _lifeTime = lifeTime;
        _owner = owner;
        _listener = listener ?? Default;
    }
    
    public virtual Item? OnPickup(Player player)
    {
        if (player.Inventory.HasSpaceFor(_item))
        {
            player.Inventory.Transaction.AddItem(_item);
            Log.Info($"added {_item} to {player.Username} from floor item.");
            if (ticksAlive <= _lifeTime)
            {
                Destroyed = true;
            }
            _listener.DestroyedFloorItem(this);
            return _item;
        }
        player.SendMessage("You don't have enough space in your inventory for that item!");
        return null;
    }

    public override void Process()
    {
        if (Destroyed)
        {
            return;
        }
        
        _lifeTime++;
    }
}