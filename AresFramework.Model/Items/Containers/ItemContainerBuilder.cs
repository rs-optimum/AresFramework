using AresFramework.Model.Items.Containers.Actions;

namespace AresFramework.Model.Items.Containers;

public class ItemContainerBuilder
{
    private int Capacity { get; set; }
    private ItemContainer.ItemStackPolicy ContainerStackPolicy = ItemContainer.ItemStackPolicy.Default;
    private ItemContainer.ItemMovePolicy ContainerMovePolicy = ItemContainer.ItemMovePolicy.Swap;
    private readonly List<IContainerListener> Listeners = new List<IContainerListener>();
    private readonly Dictionary<int, Item> PrepopulateItems = new Dictionary<int, Item>();

    public ItemContainerBuilder(int capacity)
    {
        Capacity = capacity;
    }
    
    public ItemContainer Build()
    {
        var itemContainer = new ItemContainer(Capacity, ContainerStackPolicy, ContainerMovePolicy);
        itemContainer.Transaction = LenientContainerTransaction.DefaultLenientContainerTransaction(itemContainer);
        
        foreach (var listener in Listeners)
        {
            itemContainer.AddListener(listener);
        }
        foreach (var item in PrepopulateItems)
        {
            itemContainer.Set(item.Key, item.Value);
        }

        return itemContainer;
    }

    public ItemContainerBuilder WithItemStackPolicy(ItemContainer.ItemStackPolicy itemStackPolicy)
    {
        ContainerStackPolicy = itemStackPolicy;
        return this;
    }
    
    public ItemContainerBuilder WithItemMovePolicy(ItemContainer.ItemMovePolicy itemMovePolicy)
    {
        ContainerMovePolicy = itemMovePolicy;
        return this;
    }

    public ItemContainerBuilder AddListener(IContainerListener listener)
    {
        Listeners.Add(listener);
        return this;
    }

    public ItemContainerBuilder WithItem(int slot, Item item)
    {
        PrepopulateItems.Add(slot, item);
        return this;
    }
    
    
}