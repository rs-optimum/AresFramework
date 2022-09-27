namespace AresFramework.Model.Plugins;

/// <summary>
/// This is the base class for attaching id's to specific actions, most classes that attatch to items/npcs/objects will
/// inherit this
/// </summary>
public interface IAttachableIds
{
    public int[] AttachedIds();
}