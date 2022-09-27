using AresFramework.Model.Items.Containers.Listeners;
using AresFramework.Model.Items.Containers.Transactions.Impl;

namespace AresFramework.Model.Items.Containers;

public class ItemContainerBuilder
{
    private readonly int _capacity;
    private ItemContainer.ItemStackPolicy _containerStackPolicy = ItemContainer.ItemStackPolicy.Default;
    private ItemContainer.ItemMovePolicy _containerMovePolicy = ItemContainer.ItemMovePolicy.Swap;
    private readonly List<IContainerListener> _listeners = new List<IContainerListener>();
    private readonly Dictionary<int, Item> _prepopulateItems = new Dictionary<int, Item>();

    public ItemContainerBuilder(int capacity)
    {
        _capacity = capacity;
    }
    
    public ItemContainer Build()
    {
        var itemContainer = new ItemContainer(_capacity, _containerStackPolicy, _containerMovePolicy);
        itemContainer.Transaction = LenientContainerTransaction.DefaultLenientContainerTransaction(itemContainer);
        
        foreach (var listener in _listeners)
        {
            itemContainer.AddListener(listener);
        }
        foreach (var item in _prepopulateItems)
        {
            itemContainer.Set(item.Key, item.Value);
        }

        return itemContainer;
    }
    
    public ItemContainerBuilder WithItemStackPolicy(ItemContainer.ItemStackPolicy itemStackPolicy)
    {
        _containerStackPolicy = itemStackPolicy;
        return this;
    }
    
    public ItemContainerBuilder WithItemMovePolicy(ItemContainer.ItemMovePolicy itemMovePolicy)
    {
        _containerMovePolicy = itemMovePolicy;
        return this;
    }
    
    public ItemContainerBuilder AddListener(IContainerListener listener)
    {
        _listeners.Add(listener);
        return this;
    }

    public ItemContainerBuilder WithItem(int slot, Item item)
    {
        _prepopulateItems.Add(slot, item);
        return this;
    }
    
    
}