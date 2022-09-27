using AresFramework.Cache.Model.Definitions;

namespace AresFramework.ServiceDependency.Definitions;

public interface INpcDefinitions
{
    public NpcDefinition? Get(int id);
}