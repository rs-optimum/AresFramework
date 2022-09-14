using System.Linq;
using AresFramework.Model.Items.Containers;
using AresFramework.Model.Items.Containers.Actions;
using NUnit.Framework;

namespace AresFramework.Model.Tests.Entity.Items.Containers.Actions;

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
        Assert.AreEqual(4151, result.FirstItem()?.Id);
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
        Assert.AreEqual(4151, result.FirstItem()?.Id);
        Assert.AreEqual(2, result.FirstItem()?.Amount);
        Assert.AreEqual(2, container.Count(4151));
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
    
}