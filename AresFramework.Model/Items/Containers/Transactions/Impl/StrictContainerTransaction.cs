namespace AresFramework.Model.Items.Containers.Transactions.Impl;

/// <summary>
/// A strict default containers action that will not effect the item container unless
/// specific criteria is hit
/// </summary>
public class StrictLenientContainerTransaction : LenientContainerTransaction
{
    public static ContainerTransaction DefaultStrictActions(ItemContainer container)
    {
        return new LenientContainerTransaction(container);
    }
    
    public StrictLenientContainerTransaction(ItemContainer container) : base(container)
    {
    }
    
    /// <summary>
    /// Pre checks if the person can add an item or not
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public override ContainerTransactionResult AddItem(Item item)
    {
        var canAdd = Container.HasSpaceFor(item);
        return !canAdd ? new ContainerTransactionResult(ContainerTransactionRequestState.NotEnoughSpace) : base.AddItem(item);
    }


    public override ContainerTransactionResult AddItems(params Item[] items)
    {
        var canAdd = Container.HasSpaceFor(items);
        return !canAdd ? new ContainerTransactionResult(ContainerTransactionRequestState.NotEnoughSpace) : base.AddItems(items);
    }
    
}