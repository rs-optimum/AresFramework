using AresFramework.GameEngine.Tasks;
using AresFramework.Model.Entity.Skills;
using AresFramework.Model.World;

namespace AresFramework.Model.Entity;

public class Player : Entity
{
    public readonly string Username;

    public ScheduledTask CurrentAction;

    public SkillSet SkillSet { get; set; }

    public Player(string username, Position position) : base(position)
    {
        Username = username;
        SkillSet = SkillSet.DefaultSkillSet();
    }
    
    public void SendMessage(string message)
    {
        Console.WriteLine($"{Username}: {message}");
    }

    public override void Process()
    {
        
    }
}