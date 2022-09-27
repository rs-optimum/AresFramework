using AresFramework.Cache.Model.Definitions;

namespace AresFramework.ServiceDependency.Definitions;

public interface IItemDefinitions
{
    public ItemDefinition? Get(int id);

    public ItemDefinition GetOrDefault(int id);
}