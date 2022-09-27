using AresFramework.Cache.Model.Definitions;
using AresFramework.ServiceDependency.Definitions;

namespace AresFramework.Cache.Definitions;

/// <summary>
/// A list of all npc definitions
/// </summary>
public class NpcDefinitions : INpcDefinitions
{
    private readonly Dictionary<int, NpcDefinition> _definitions = new();
    
    public NpcDefinitions()
    {
        _definitions.Add(1, new NpcDefinition(1, "Abyssal Whip"));
        _definitions.Add(2, new NpcDefinition(2, "Coins"));
    }
    
    public static readonly NpcDefinition Default = new(0, "Unknown");
    
    public NpcDefinition? Get(int itemId)
    {
        _definitions.TryGetValue(itemId, out var def);
        return def;
    }
        
    public NpcDefinition GetOrDefault(int itemId)
    {
        _definitions.TryGetValue(itemId, out var def);
        if (def == null)
        {
            return Default;
        }
        return def;
    }
    
}