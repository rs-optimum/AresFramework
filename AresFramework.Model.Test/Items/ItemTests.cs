using AresFramework.Model.Items;

namespace AresFramework.Model.Test.Items;

public class ItemTests
{

    [Test]
    public void HasDefinition_HasDef_ReturnTrue()
    {
        var item = new Item(995, 100);
        Assert.True(item.HasDefinition());
    }


    [Test]
    public void IncrementAmount_Add1_ReturnCorrectAmount()
    {
        var item = new Item(995, 100);

        var newAmount = item.IncrementAmount(100);
        
        Assert.AreEqual(100, item.Amount);
        Assert.AreEqual(200, newAmount!.Amount);
    }
    
    [Test]
    public void DecrementAmount_Add1_ReturnCorrectAmount()
    {
        var item = new Item(995, 100);
        var newAmount = item.IncrementAmount(100);
        
        Assert.AreEqual(100, item.Amount);
        Assert.AreEqual(200, newAmount!.Amount);
    }
    

    [Test]
    public void HasDefinition_NoDef_ReturnFalse()
    {
        var item = new Item(-1, 100);
        Assert.False(item.HasDefinition());
    }

    [Test]
    public void ToString_UnknownDef_ReturnCorrectly()
    {
        var item = new Item(-1, 100);
        
        if (ServiceDependency.AresServiceCollection.ServerSettings != null)
            ServiceDependency.AresServiceCollection.ServerSettings.EnableGameDebug = false;
        var result = item.ToString();
        
        Assert.AreEqual("100x Unknown", result);
    }
    
    
    [Test]
    public void ToString_KnownDefDisableGameDebug_ReturnCorrectly()
    {
        var item = new Item(995, 100);

        if (ServiceDependency.AresServiceCollection.ServerSettings != null)
            ServiceDependency.AresServiceCollection.ServerSettings.EnableGameDebug = false;

        var result = item.ToString();
        Assert.AreEqual("100x Coins", result);
    }

    [Test]
    public void ToString_KnownDefEnableGameDebug_ReturnsCorrectly()
    {
        var item = new Item(995, 100);
        
        if (ServiceDependency.AresServiceCollection.ServerSettings != null)
            ServiceDependency.AresServiceCollection.ServerSettings.EnableGameDebug = true;

        var result = item.ToString();
        Assert.AreEqual("100x Coins (995)", result); 
    }
    
    
    
}