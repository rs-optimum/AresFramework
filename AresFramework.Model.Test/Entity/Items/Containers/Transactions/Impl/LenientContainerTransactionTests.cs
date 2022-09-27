using AresFramework.Model.Items.Containers;
using AresFramework.Model.Items.Containers.Transactions;
using AresFramework.Model.Items.Containers.Transactions.Impl;

namespace AresFramework.Model.Test.Entity.Items.Containers.Transactions.Impl;

public class LenientContainerTransactionTests
{
    
    [Test]
    public void AddItem_AddsValidItemNonStackable_ReturnCorrectly()
    {
        var container = new ItemContainerBuilder(2)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        var result = container.Transaction.AddItem(4151);

        Assert.AreEqual(ContainerTransactionRequestState.Success, result.State);
        Assert.AreEqual(4151, result.FirstSuccessfulItem()?.Id);
        Assert.AreEqual(1, container.Count(4151));
    }
    
    
    [Test]
    public void AddItem_AddsValidItemsNonStackableNonStrict_ReturnCorrectly()
    {
        var container = new ItemContainerBuilder(2)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        var result = container.Transaction.AddItem(4151, 3);

        Assert.AreEqual(ContainerTransactionRequestState.NotEnoughSpace, result.State);
        Assert.AreEqual(4151, result.FirstSuccessfulItem()?.Id);
        Assert.AreEqual(2, result.FirstSuccessfulItem()?.Amount);
        Assert.AreEqual(2, container.Count(4151));
    }
    
    
    
    [Test]
    public void AddItem_AddsValidItemsNonStackableNonStrictFull_ReturnCorrectly()
    {
        var container = new ItemContainerBuilder(1)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        container.Transaction.AddItem(4151);
        
        var result = container.Transaction.AddItem(4151, 1);

        Assert.AreEqual(ContainerTransactionRequestState.NotEnoughSpace, result.State);
        Assert.AreEqual(null, result.FirstSuccessfulItem());
    }
    
    
    [Test]
    public void AddItem_AddsValidItemsNonStackableStrict_ReturnNotEnoughSpace()
    {
        var container = new ItemContainerBuilder(2)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        container.Transaction = new StrictLenientContainerTransaction(container);
        
        var result = container.Transaction.AddItem(4151, 3);

        Assert.AreEqual(ContainerTransactionRequestState.NotEnoughSpace, result.State);
        Assert.AreEqual(0, result.Successful.Count);
    }
    
    
    [Test]
    public void AddItem_AddsValidItemsStackableLenient_ReturnAdded()
    {
        var container = new ItemContainerBuilder(1)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        container.Transaction.AddItem(995, int.MaxValue);
        var adding1More = container.Transaction.AddItem(995, 1);
        
        Assert.AreEqual(ContainerTransactionRequestState.Overflow, adding1More.State);
        Assert.AreEqual(null, adding1More.FirstSuccessfulItem());
    }
    
}