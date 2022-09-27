using AresFramework.Model.World;

namespace AresFramework.Model.Entities;

public class TestEntity : Entity
{
    
    public TestEntity(Position position) : base(position)
    {
        
    }

    public override void Process()
    {
        Console.WriteLine("test");
    }
}