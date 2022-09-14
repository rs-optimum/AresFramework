namespace AresFramework.Model.Items.Containers;

public class Inventory
{
    public int InventoryType { get; set; }

    private ItemContainer Container { get; set; }

    public Inventory(int inventoryType)
    {
        var capacity = 10; // Cache.Inventory.GetType(inventoryType).Capacity;
        
    }

}