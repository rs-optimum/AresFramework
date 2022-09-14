using AresFramework.Cache.Model.Definitions;
using AresFramework.ServiceDependencies.Definitions;

namespace AresFramework.Cache.Items;

public class ItemDefinitions : IItemDefinitions
{
    private readonly Dictionary<int, ItemDefinition> _definitions = new Dictionary<int, ItemDefinition>();
    
    public ItemDefinitions()
    {
        _definitions.Add(4151, new ItemDefinition(4151, "Abyssal Whip", false));
        _definitions.Add(995, new ItemDefinition(995, "Coins", true));
    }
    
    public ItemDefinition? Get(int itemId)
    {
        _definitions.TryGetValue(itemId, out var def);
        return def;
    }
    
}