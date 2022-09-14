using AresFramework.Model.World;

namespace AresFramework.Model.Entity;

public class Npc : Entity
{
    public readonly int Id;

    public Npc(int id, Position position) : base(position)
    {
        this.Id = id;
    }

    public override void Process()
    {
        throw new NotImplementedException();
    }
}