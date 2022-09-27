using AresFramework.Model.Entities;
using AresFramework.Model.Entities.Players;

namespace AresFramework.Model.Items.Containers.Listeners;

/// <summary>
/// A player inventory listener, used for updating transactions
/// </summary>
public class PlayerInventoryListener : IContainerListener
{
    private readonly Player _player;
    public PlayerInventoryListener(Player player)
    {
        _player = player;
    }


    public void ItemUpdate(ItemContainer container, int slot, Item item)
    {
        throw new NotImplementedException();
    }

    public void ItemsUpdate(ItemContainer container)
    {
        throw new NotImplementedException();
    }
}