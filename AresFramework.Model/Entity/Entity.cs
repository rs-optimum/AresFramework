using AresFramework.Model.World;

namespace AresFramework.Model.Entity;

public abstract class Entity
{
    public Position Position;

    protected Entity(Position position)
    {
        Position = position;
    }

    public abstract void Process();
}