using AresFramework.Cache;
using AresFramework.Model.Entities;
using AresFramework.Model.Entities.Npcs;

namespace AresFramework.Model.World;

/// <summary>
/// Represents the whole game world
/// </summary>
public class GameWorld
{

    public List<Npc> Npcs = new List<Npc>();
    
    
    
    
    public GameCache Cache { get; set; }

    
    

}