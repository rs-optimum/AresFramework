using AresFramework.Model.Items.Containers.Transactions;

namespace AresFramework.Model.Items.Containers;

/// <summary>
/// A generic inventory class that's derived from the cache config for inventories
/// </summary>
public sealed class Inventory : ItemContainer
{
    /// <summary>
    /// The inventory type
    /// </summary>
    public int InventoryType { get; set; }

    public Inventory(int inventoryType) : base()
    {
        var capacity = 10; // Cache.Inventory.GetType(inventoryType).Capacity;
        
    }
    

    public ContainerTransactionResult AddItem(Item item, ContainerTransaction? transaction = null)
    {
        transaction ??= Transaction;
        return transaction.AddItem(item);
    }
    
    public ContainerTransactionResult AddItem(int id, int amount, ContainerTransaction? transaction = null)
    {
        transaction ??= Transaction;
        return transaction.AddItem(id, amount);
    }

}