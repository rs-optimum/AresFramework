using AresFramework.Cache.Model.Definitions;

namespace AresFramework.ServiceDependencies.Definitions;

public interface IItemDefinitions
{
    
    public ItemDefinition? Get(int id);
    
}