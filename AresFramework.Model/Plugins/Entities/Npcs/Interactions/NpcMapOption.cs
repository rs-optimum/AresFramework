namespace AresFramework.Model.Plugins.Entities.Npcs.Interactions;

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