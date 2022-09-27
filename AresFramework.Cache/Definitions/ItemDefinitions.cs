using AresFramework.Cache.Model.Definitions;
using AresFramework.ServiceDependency.Definitions;

namespace AresFramework.Cache.Definitions;

public class ItemDefinitions : IItemDefinitions
{
    private readonly Dictionary<int, ItemDefinition> _definitions = new Dictionary<int, ItemDefinition>();
    
    public ItemDefinitions()
    {
        _definitions.Add(4151, new ItemDefinition(4151, "Abyssal Whip", false));
        _definitions.Add(995, new ItemDefinition(995, "Coins", true));
    }
    
    public static readonly ItemDefinition Default = new(0, "Unknown", false);
    
    public ItemDefinition? Get(int itemId)
    {
        _definitions.TryGetValue(itemId, out var def);
        return def;
    }
    
    public ItemDefinition GetOrDefault(int itemId)
    {
        _definitions.TryGetValue(itemId, out var def);

        if (def == null)
        {
            return Default;
        }
        
        return def;
    }
    
}