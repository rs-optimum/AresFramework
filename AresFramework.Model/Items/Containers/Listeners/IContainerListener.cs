namespace AresFramework.Model.Items.Containers.Listeners;

/// <summary>
/// Hooks into an item container and receives information on what happened in the item container
/// </summary>
public interface IContainerListener
{
    /// <summary>
    /// Called when a specific item is updated
    /// </summary>
    /// <param name="container">The container we are updating from</param>
    /// <param name="slot">the slot of the item changed</param>
    /// <param name="item">The item that changed</param>
    void ItemUpdate(ItemContainer container, int slot, Item item);
    
    /// <summary>
    /// Multiple items have updated
    /// </summary>
    /// <param name="container"></param>
    void ItemsUpdate(ItemContainer container);
}