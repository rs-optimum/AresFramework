using AresFramework.Model.Items.Containers.Listeners;
using AresFramework.Model.Items.Containers.Transactions;
using AresFramework.Model.Items.Containers.Transactions.Impl;

namespace AresFramework.Model.Items.Containers;

/// <summary>
/// An abstract generic class for an item container
/// </summary>
public class ItemContainer
{
    public Item[] Items { get; set; }
    private int _capacity;
    private ItemStackPolicy _itemStackPolicy;
    private ItemMovePolicy _itemMovePolicy;
    private List<IContainerListener> _listeners = new();
    public ContainerTransaction Transaction;
    
    /// <summary>
    /// The current size of the container
    /// </summary>
    private int CurrentSize => Items.Count(e => e != null);

    public ItemContainer(int capacity, ItemStackPolicy itemStackPolicy, ItemMovePolicy policy)
    {
        Items = new Item[capacity];
        _capacity = capacity;
        _itemStackPolicy = itemStackPolicy;
        _itemMovePolicy = policy;
        Transaction = new LenientContainerTransaction(this);
    }

    public ItemContainer()
    {
        Transaction = new LenientContainerTransaction(this);
    }

    protected void SetCapacity(int capacity)
    {
        Items = new Item[capacity];
    }

    /// <summary>
    /// Attempts to clear the container and reset all the items
    /// </summary>
    public void ClearContainer()
    {
        Items = new Item[_capacity];
        _listeners.ForEach(e => e.ItemsUpdate(this));
    }

    /// <summary>
    /// Gets an item in a specific slot
    /// </summary>
    /// <param name="slot">The slot we are checking an item for</param>
    /// <returns></returns>
    public Item? Get(int slot)
    {
        if (slot < 0 || slot >= _capacity)
        {
            return null;
        }

        return Items[slot];
    }
    
    
    /// <summary>
    /// Gets the slot of an item id
    /// </summary>
    /// <param name="item">The item id we are getting a slot for</param>
    /// <returns>A nullable slot id</returns>
    public int? GetSlotForItem(int id)
    {
        for(var i = 0; i < Items.Length; i++)
        {
            var found = Items[i];
            if (found == null) continue;
           
            if (found.Id == id)
            {
                return i;
            }
        }
        return null;
    }

    public int? GetFirstFreeSlot()
    {
        if (GetFreeSlots() <= 0) return null;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                return i;
            }
        }

        return -1;
    }
    
    /// <summary>
    /// Gets the slot of an item id
    /// </summary>
    /// <param name="item">The item we are getting a slot for</param>
    /// <returns>A nullable slot id</returns>
    public int? GetSlotForItem(Item item)
    {
        return GetSlotForItem(item.Id);
    }
    
    /// <summary>
    /// Checks if a specific slot contains an item
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(int slot, Item item)
    {
        if (slot < 0 || slot >= _capacity)
        {
            return false;
        }

        var itemInSlot = Items[slot];
        if (itemInSlot == null)
        {
            return false;
        }

        if (itemInSlot.Id != item.Id)
        {
            return false;
        }
        
        return itemInSlot.Amount >= item.Amount;
    }

    /// <summary>
    /// Check if the item container contains a specific amount of items
    /// </summary>
    /// <param name="item">The items to check for</param>
    /// <returns>True if the item container contains the item, false if not</returns>
    public bool Contains(Item item)
    {
        var amountFound = 0;
        foreach (var i in Items)
        {
            if (i == null) continue;
            
            if (i.Id == item.Id)
            {
                amountFound += i.Amount;
                if (amountFound >= item.Amount)
                {
                    return true;
                }
            }
        }
        return amountFound >= item.Amount;
    }
    
    /// <summary>
    /// Checks if the <see cref="ItemContainer"/> contains multiple <see cref="Item"/>'s
    /// </summary>
    /// <param name="items">The array of items</param>
    /// <returns>True if the container contains all the items</returns>
    public bool Contains(params Item[] items)
    {
        foreach (var item in items)
        {
            if (item == null) continue;
            
            var contains = Contains(item);
            if (!contains)
            {
                return false;
            }
        }
        return true;
    }
    
    /// <summary>
    /// Checks if the <see cref="ItemContainer"/> contains multiple <see cref="Item"/>'s
    /// </summary>
    /// <param name="items">The array of item id's</param>
    /// <returns>True if the container contains all the items</returns>
    public bool Contains(params int[] items)
    {
        foreach (var item in items)
        {
            if (item == null) continue;
            var contains = Contains(new Item(item));
            if (!contains)
            {
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// Checks if the container has space for a list of items
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public bool HasSpaceFor(params Item[] items)
    {
        int spaceRequired = 0;
        foreach (var item in items)
        {
            if (item == null) continue;
            spaceRequired += GetRequiredSlotsFor(item);
        }
        return GetFreeSlots() >= spaceRequired;
    }

    public void AddListener(IContainerListener listener)
    {
        _listeners.Add(listener);
    }
    
    public int Count(int itemId)
    {
        int amountFound = 0;
        foreach (var item in Items)
        {
            if (item == null) continue;

            if (item.Id == item)
            {
                amountFound += item.Amount;
            }
        }

        return amountFound;
    }
    
    public bool HasSpaceFor(Item item)
    {
        return HasSpaceFor(new[] {item});
    }
    
    /// <summary>
    /// Gets the required space for an item
    /// </summary>
    /// <param name="item">The required space for an item</param>
    /// <returns>the amount of slots required</returns>
    public int GetRequiredSlotsFor(Item item)
    {
        int spaceRequired = 0;
        
        // Here we will slightly modify the stack policy based
        // on what the items stack is
        var stackPolicy = GetItemStackPolicy(item);
        
        if (stackPolicy == ItemStackPolicy.Always)
        {
            if (!Contains(item))
            {
                spaceRequired += 1;
            }
        }
        else if (stackPolicy == ItemStackPolicy.Never)
        {
            spaceRequired += item.Amount;
        }
        return spaceRequired;
    }
    
    public int GetFreeSlots()
    {
        var freeSlots = _capacity - CurrentSize;
        return freeSlots;
    }
    
    
    public ItemStackPolicy GetItemStackPolicy(Item item)
    {
        var stackPolicy = _itemStackPolicy;
        if (stackPolicy == ItemStackPolicy.Default)
        {
            stackPolicy = ItemContainerUtilities.GetStackPolicyForItem(item);
        }
        return stackPolicy;
    }


    public List<IContainerListener> Listeners => _listeners;


    #region Sets items

    /// <summary>
    /// This will set a specific slot to an item ignoring <see cref="ItemStackPolicy"/> and <see cref="ItemMovePolicy"/>
    /// such as stackable
    /// </summary>
    /// <param name="slot">the slot to set</param>
    /// <param name="item">The item to set</param>
    public void Set(int slot, Item item)
    {
        if (slot < 0 || slot >= _capacity)
        {
            return;
        }

        Items[slot] = item;
        _listeners.ForEach(e => e.ItemUpdate(this, slot, item));
    }
    
    
    /// <summary>
    /// <inheritdoc cref="Set(int,Item)"/>
    /// </summary>
    /// <param name="slot">The slot to set</param>
    /// <param name="item">The item id to set</param>
    public void Set(int slot, int item)
    {
        Set(slot, new Item(item));
    }
    
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void Set(int slot, int item, int amount)
    {
        Set(slot, new Item(item, amount));
    }

    #endregion

    #region Policy Enums
    /// <summary>
    /// Descibes the stacking policy on the items in an item container
    /// </summary>
    public enum ItemStackPolicy
    {
        /// <summary>
        /// Will always stack regardless of the item (apart from items with attributes)
        /// </summary>
        Always,
        
        /// <summary>
        /// Uses the items default stack policy
        /// </summary>
        Default,
        
        
        /// <summary>
        /// Will never stack items, not even item policy stacks
        /// </summary>
        Never
    }
        
    /// <summary>
    /// Describes how a container's item's will behave when moved 
    /// </summary>
    public enum ItemMovePolicy
    {
        /// <summary>
        /// When an item moves, all items will be shifted to make space
        /// </summary>
        Shift,
        
        /// <summary>
        /// Swaps positions for 2 items
        /// </summary>
        Swap
    }
    #endregion


    
}