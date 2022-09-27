namespace AresFramework.Model.Items.Containers.Transactions.Impl;

/// <summary>
/// A default implementation of <see cref="ContainerTransaction"/>. This allows adding multiple items and stopping when no more space
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
        var transactionResult = new ContainerTransactionResult(ContainerTransactionRequestState.Success,  new List<Item>());
        
        // When the container ALWAYS stacks
        if (policy == ItemContainer.ItemStackPolicy.Always)
        {
            var slot = Container.GetSlotForItem(item);
            
            // If the item doesn't exist
            if (slot == null)
            {
                var freeSlot = Container.GetFirstFreeSlot();
                if (freeSlot == null)
                {
                    transactionResult.State = ContainerTransactionRequestState.NotEnoughSpace;
                    return transactionResult;
                }
                Container.Set(freeSlot.Value, item);
                transactionResult.Successful.Add(item);
                return transactionResult;
            }
            
            // The item exists
            Item containerItem = Container.Items[slot.Value];
            var addedAmount = containerItem.IncrementAmount(item.Amount);
            
            // This means an overflow error, so we will add only the amount we can
            if (addedAmount == null)
            {
                var maxAmountToAdd = int.MaxValue - containerItem.Amount;
                transactionResult.State = ContainerTransactionRequestState.Overflow;

                var newItem = containerItem.IncrementAmount(maxAmountToAdd)!;
                item.DecrementAmount(maxAmountToAdd);
                
                // Only add successful item if it's above the threshold
                if (maxAmountToAdd > 0)
                {
                    transactionResult.Successful.Add(new Item(item.Id, maxAmountToAdd));
                }
                
                Container.Set(slot.Value, newItem);
                return transactionResult;
            }
            
            Container.Set(slot.Value, addedAmount);
            transactionResult.Successful.Add(addedAmount);
            
            return transactionResult;
        }
        
        
        var itemsAdded = new Item(item.Id, 0);
        for (int i = 0; i < item.Amount; i++)
        {
            var findFreeSlot = Container.GetFirstFreeSlot();
            if (findFreeSlot == null)
            {
                transactionResult.State = ContainerTransactionRequestState.NotEnoughSpace;
                if (itemsAdded!.Amount > 0)
                {
                    transactionResult.Successful.Add(itemsAdded!);
                }
                return transactionResult;
            }
            
            itemsAdded = itemsAdded!.IncrementAmount(1);
            Container.Set(findFreeSlot.Value, new Item(item.Id));
        }
        
        // Added successfully
        transactionResult.Successful.Add(itemsAdded!);
        return transactionResult;
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

    public override ContainerTransactionResult RemoveItem(int id, int amount)
    {
        throw new NotImplementedException();
    }
}