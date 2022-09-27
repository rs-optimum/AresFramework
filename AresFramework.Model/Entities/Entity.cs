using AresFramework.Model.World;

namespace AresFramework.Model.Entities;

public abstract class Entity
{
    public Position Position;

    /// <summary>
    /// Signals that this entity has been destroyed and shall be removed from the main game
    /// </summary>
    public bool Destroyed;

    protected Entity(Position position)
    {
        Position = position;
    }
    
    public abstract void Process();
}