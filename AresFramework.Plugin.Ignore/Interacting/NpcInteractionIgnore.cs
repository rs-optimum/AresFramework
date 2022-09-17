namespace AresFramework.Plugin.Ignore.Interacting;

/// <summary>
/// Defined to ignore a specific npc id and option from a plugin repo
/// </summary>
public class NpcInteractionIgnore
{
    public string Option { get; set; }
    public int NpcId { get; set; }

    public NpcInteractionIgnore(string option, int npcId)
    {
        Option = option;
        NpcId = npcId;
    }

    public NpcInteractionIgnore()
    {
    }


    public override bool Equals(object? obj)
    {
        var other = obj as NpcInteractionIgnore;
        return other.Option == Option && other.NpcId == NpcId;
    }
}