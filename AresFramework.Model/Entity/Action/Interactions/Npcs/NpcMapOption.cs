namespace AresFramework.Model.Entity.Action.Interactions.Npcs;

public record NpcMapOption
{
    public readonly int Id;
    public readonly string Option;

    public NpcMapOption(int id, string option)
    {
        Id = id;
        Option = option;
    }
}