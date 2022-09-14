namespace AresFramework.Model.Items.Containers.Actions;

/// <summary>
/// A default implementation of <see cref="ContainerTransaction"/>, most of these actions
/// do not regard adding "multiple" items without stopping
/// </summary>
public class LenientContainerTransaction : ContainerTransaction
{
    public static ContainerTransaction DefaultLenientContainerTransaction(ItemContainer container)
    {
        return new LenientContainerTransaction(container);
    }
    
    public LenientContainerTransaction(ItemContainer container) : base(container)
    {
    }

    public override ContainerTransactionResult AddItem(Item item)
    {
        var policy = Container.GetItemStackPolicy(item);
        if (policy == ItemContainer.ItemStackPolicy.Always)
        {
            var slot = Container.GetSlotForItem(item);
            
            if (slot == null)
            {
                var freeSlot = Container.GetFirstFreeSlot();
                if (freeSlot == null)
                {
                    return new ContainerTransactionResult(ContainerTransactionRequestState.NotEnoughSpace);
                }
                Container.Set(freeSlot.Value, item);
                return new ContainerTransactionResult(ContainerTransactionRequestState.Success, new List<Item>() { item });
            }
            
            Item containerItem = Container.Items[slot.Value];
            var currentCount = containerItem.Amount;
            if (((long) currentCount + item.Amount) > int.MaxValue)
            {
                return new ContainerTransactionResult(ContainerTransactionRequestState.Overflow);
            }
                    
            containerItem = new Item(item.Id, currentCount + item.Amount);
            Container.Set(slot.Value, containerItem);
            return new ContainerTransactionResult(ContainerTransactionRequestState.Success, new List<Item>() {item});
        }
        
        
        var itemsAdded = new Item(item.Id, 0);
        
        // The policy here is never stack...
        for (int i = 0; i < item.Amount; i++)
        {
            var findFreeSlot = Container.GetFirstFreeSlot();
            if (findFreeSlot == null)
            {
                return new ContainerTransactionResult(ContainerTransactionRequestState.NotEnoughSpace, new List<Item>() { itemsAdded });
            }
            
            itemsAdded = itemsAdded.IncrementAmount(1);
            Container.Set(findFreeSlot.Value, new Item(item.Id));
        }
        
        return new ContainerTransactionResult(ContainerTransactionRequestState.Success, new List<Item>() { itemsAdded });
    }

    public override ContainerTransactionResult AddItem(int id)
    {
        return AddItem(new Item(id));
    }

    public override ContainerTransactionResult AddItem(int id, int amount)
    {
        return AddItem(new Item(id, amount));
    }

    public override ContainerTransactionResult AddItems(params Item[] items)
    {
        var result = new ContainerTransactionResult(ContainerTransactionRequestState.Success);
        foreach (var item in items)
        {
            var addResult = AddItem(item);

            if (addResult.State == ContainerTransactionRequestState.Success)
            {
                result.Successful.Add(item);
            }
            else
            {
                result.Successful.AddRange(addResult.Successful);
                result.State = addResult.State;
                return result;
            }
        }
        return result;
    }

    public override ContainerTransactionResult RemoveItem(Item item)
    {
        throw new NotImplementedException();
    }

    public override ContainerTransactionResult RemoveItem(int id)
    {
        throw new NotImplementedException();
    }

    public override ContainerTransactionResult RemoveItem(int id, int amount)
    {
        throw new NotImplementedException();
    }
}