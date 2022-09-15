using AresFramework.Model.Items;
using AresFramework.Model.Items.Containers;
using AresFramework.Model.Items.Containers.Actions;
using NUnit.Framework;

namespace AresFramework.Model.Tests.Entity.Items.Containers;

public class ItemContainerTests
{
    
    [Test]
    public void Set_SetSlot_CorrectlySetSlotItem()
    {
        var itemToSet = new Item(995, 200_000);
        
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .WithItem(0, itemToSet)
            .Build();

        container.Set(0, itemToSet);
        var get = container.Get(0);
        var get2 = container.Get(1);
        
        Assert.AreEqual(true, container.Contains(itemToSet));
        Assert.NotNull(get);
        Assert.IsNull(get2);
        Assert.IsTrue(false);
    }
    
    [Test]
    public void Set_SetSlotSlotParamBoundaries_DoNothing()
    {
        var itemToSet = new Item(995, 200_000);
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        container.Set(-1, itemToSet);
        container.Set(28, itemToSet);

        Assert.AreEqual(28, container.GetFreeSlots());
    }
    
    [Test]
    public void GetRequiredSlotsFor_NonStackableItemsDefaultStackPolicy_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        var item = container.Items;
        
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(4151)));
        Assert.AreEqual(2, container.GetRequiredSlotsFor(new Item(4151, 2)));
    }
    
        
    [Test]
    public void GetRequiredSlotsFor_StackableItemsDefaultStackPolicy_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(995)));
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(995, 200_000)));
    }
    
    
    [Test]
    public void GetRequiredSlotsFor_NonStackableItemsAlwaysStackPolicy_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Always)
            .Build();
        
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(4151, 1)));
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(4151, 200_000)));
    }
    
    [Test]
    public void GetRequiredSlotsFor_StackableItemsAlwaysStackPolicy_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Always)
            .Build();
        
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(995, 1)));
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(995, 200_000)));
    }

    
    [Test]
    public void GetRequiredSlotsFor_StackableItemsNeverStackPolicy_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Never)
            .Build();
        
        Assert.AreEqual(2, container.GetRequiredSlotsFor(new Item(995, 2)));
    }
    
    [Test]
    public void GetRequiredSlotsFor_NonStackableItemsNeverStackPolicy_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Never)
            .Build();
        
        Assert.AreEqual(2, container.GetRequiredSlotsFor(new Item(4151, 2)));
    }
    
    
    [Test]
    public void GetRequiredSlotsFor_StackableItemsDefaultStackPolicyContainsItem_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        container.Set(0, new Item(995, 200_000));
        Assert.AreEqual(0, container.GetRequiredSlotsFor(new Item(995, 200_000)));
    }
    
    [Test]
    public void GetRequiredSlotsFor_NonStackableItemsDefaultStackPolicyContainsItem_ReturnsCorrectAmount()
    {
        var container = new ItemContainerBuilder(28)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        container.Set(0, new Item(4151, 1));
        Assert.AreEqual(1, container.GetRequiredSlotsFor(new Item(4151, 1)));
    }
    
    [Test]
    public void HasSpaceFor_NonStackableItemsDefaultStackPolicyContainsItem_ReturnsFalse()
    {
        var container = new ItemContainerBuilder(1)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        container.Set(0, new Item(4151, 1));
        
        Assert.AreEqual(false, container.HasSpaceFor(new Item(4151)));
    }
    
    [Test]
    public void HasSpaceFor_StackableItemsDefaultStackPolicyContainsItem_ReturnsTrue()
    {
        var container = new ItemContainerBuilder(1)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        container.Set(0, new Item(995, 1));
        
        Assert.AreEqual(true, container.HasSpaceFor(new Item(995)));
    }
    
    [Test]
    public void HasSpaceFor_MultipleStackableItemsDefaultStackPolicy_ReturnsFalse()
    {
        var container = new ItemContainerBuilder(2)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        container.Set(0, new Item(4151, 1));
        
        Assert.AreEqual(false, container.HasSpaceFor(new Item(4151), new Item(4151)));
    }
    
    [Test]
    public void HasSpaceFor_MultipleStackableItemsDefaultStackPolicy_ReturnsTrue()
    {
        var container = new ItemContainerBuilder(3)
            .WithItemMovePolicy(ItemContainer.ItemMovePolicy.Swap)
            .WithItemStackPolicy(ItemContainer.ItemStackPolicy.Default)
            .Build();
        
        container.Set(0, new Item(4151, 1));
        
        Assert.AreEqual(true, container.HasSpaceFor(new Item(4151), new Item(4151)));
    }
    
}