using AresFramework.Cache.Model.Definitions;
using AresFramework.Model.Entities;
using AresFramework.ServiceDependency;
using AresFramework.ServiceDependency.Definitions;

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
    
    /// <summary>
    /// Items have attribute maps
    /// </summary>
    public AttributesMap AttributeMap { get; set; } = new AttributesMap();

    public ItemDefinition? Definition()
    {
        var definition = AresServiceCollection.ItemDefinitions;
        if (definition == null)
        {
            return null;
        }
        return definition.Get(Id);   
    }
    
    public ItemDefinition DefinitionOrDefault()
    {
        var definition = AresServiceCollection.ItemDefinitions;
        if (definition == null)
        {
            return null;
        }
        return definition.Get(Id);   
    }

    public bool HasDefinition()
    {
        return Definition() != null;
    }

    public static implicit operator Item(int id)
    {
        return new Item(id);
    }

    /// <summary>
    /// Will try to increment the amount of the current item,
    /// returns null if unsuccessful 
    /// </summary>
    /// <param name="amount">The amount to increment</param>
    /// <returns></returns>
    public Item? IncrementAmount(int amount)
    {
        if ( ((long) amount) + ((long) Amount) > int.MaxValue)
        {
            return null;
        }
        return new Item(Id, Amount + amount);
    }
    
    /// <summary>
    /// Attempts to decrement the amount, returns null if both amounts are below 0
    /// </summary>
    /// <param name="amount">the amount to decrement by</param>
    /// <returns></returns>
    public Item? DecrementAmount(int amount)
    {
        if (amount - Amount < 0)
        {
            return null;
        }
        
        return new Item(Id, Amount - amount);
    }
    
    public override string ToString()
    {
        var name = HasDefinition() ? Definition()?.Name : "Unknown";
        var settings = AresServiceCollection.ServerSettings;
        if (settings != null && settings.EnableGameDebug)
        {
            return $"{Amount}x {name} ({Id})";
        }
        return $"{Amount}x {name}";
    }

    public Item() : this(0,0)
    {
        
    }
    
}