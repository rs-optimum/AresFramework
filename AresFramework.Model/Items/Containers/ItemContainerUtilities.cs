namespace AresFramework.Model.Items.Containers;

/// <summary>
/// Contains a bunch of utilities for ItemContainers, most operations will be handled within a container
/// </summary>
public static class ItemContainerUtilities
{ 
    /// <summary>
    /// Gets the stack policy for an item based on default
    /// </summary>
    /// <param name="item">the item we get stack policy for</param>
    /// <returns></returns>
    public static ItemContainer.ItemStackPolicy GetStackPolicyForItem(Item item)
    {
        var def = item.Definition();
        if (def == null)
        {
            return ItemContainer.ItemStackPolicy.Never;
        }
        return def.IsStackable ? ItemContainer.ItemStackPolicy.Always : ItemContainer.ItemStackPolicy.Never;
    }
}