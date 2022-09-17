using AresFramework.Cache.Model.Definitions;
using AresFramework.ServiceDependencies;
using AresFramework.ServiceDependencies.Definitions;

namespace AresFramework.Model.Items;

/// <summary>
/// Represents a GameItem
/// </summary>
/// <param name="Id">The game item id</param>
/// <param name="Amount">The game item amount</param>
public record Item(int Id, int Amount = 1)
{ 
    public int Id { get; set; } = Id;
    public int Amount { get; set; } = Amount;

    public ItemDefinition? Definition()
    {
        var definition = AresServiceCollection.ServiceProvider.GetService(typeof(IItemDefinitions)) as IItemDefinitions;
        if (definition == null)
        {
            return null;
        }
        return definition.Get(Id);   
    }

    public static implicit operator Item(int id)
    {
        return new Item(id);
    }

    public Item IncrementAmount(int amount)
    {
        return new Item(this.Id, amount + amount);
    }

    public Item() : this(0,0)
    {
        
    }
    
}