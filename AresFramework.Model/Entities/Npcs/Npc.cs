using AresFramework.Cache.Model.Definitions;
using AresFramework.Model.World;
using AresFramework.ServiceDependency;
using NLog;

namespace AresFramework.Model.Entities.Npcs;

public class Npc : Entity
{

    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public readonly int Id;
    
    public NpcDefinition? Definition()
    {
        var definition = AresServiceCollection.NpcDefinitions;
        if (definition == null)
        {
            return null;
        }
        return definition.Get(Id);   
    }

    public Npc(int id, Position position) : base(position)
    {
        this.Id = id;
    }

    public override string ToString()
    {
        var definition = Definition();
        var name = definition?.Name ?? "Unknown";
        if (AresServiceCollection.ServerSettings!.EnableGameDebug)
        {
            return $"{name} ({Id})";
        }
        return $"{name}";
    }

    public override void Process()
    {
        throw new NotImplementedException();
    }
}