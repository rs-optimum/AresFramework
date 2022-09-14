namespace AresFramework.Model.Items.Containers;

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