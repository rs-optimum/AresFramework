namespace AresFramework.Model.Items.Containers.Transactions;

/// <summary>
/// Contains generic container actions with all inherited methods
/// </summary>
public abstract class ContainerTransaction
{
    protected readonly ItemContainer Container;

    protected ContainerTransaction(ItemContainer container)
    {
        Container = container;
    }

    /// <summary>
    /// Attempts to add an item and disregard state
    /// </summary>
    /// <param name="item">The item we are attempting to add</param>
    /// <returns>A container action result</returns>
    public abstract ContainerTransactionResult AddItem(Item item);
    public abstract ContainerTransactionResult AddItem(int id, int amount);

    public abstract ContainerTransactionResult AddItems(params Item[] items);

    public abstract ContainerTransactionResult RemoveItem(Item item);
    public abstract ContainerTransactionResult RemoveItem(int id, int amount);
    

}