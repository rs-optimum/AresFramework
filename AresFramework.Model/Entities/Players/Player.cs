using AresFramework.GameEngine.Tasks;
using AresFramework.Model.Entities.Skills;
using AresFramework.Model.Items.Containers;
using AresFramework.Model.World;

namespace AresFramework.Model.Entities.Players;

public class Player : Entity
{
    public readonly string Username;

    public ScheduledTask CurrentAction;

    public SkillSet SkillSet { get; set; }

    /// <summary>
    /// The attributes map
    /// </summary>
    public AttributesMap AttributeMap { get; set; } = new AttributesMap();

    public ItemContainer Inventory { get; set; } =
        new ItemContainer(28, ItemContainer.ItemStackPolicy.Default, ItemContainer.ItemMovePolicy.Swap);

    public PlayerEquipment PlayerEquipment = new PlayerEquipment();

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