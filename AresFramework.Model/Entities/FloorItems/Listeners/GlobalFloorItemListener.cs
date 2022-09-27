namespace AresFramework.Model.Entities.FloorItems.Listeners;

/// <summary>
/// The floor items to add
/// </summary>
public class GlobalFloorItemListener : IFloorItemListener
{
    /// <summary>
    /// 
    /// </summary>
    public List<FloorItem> ToRemove = new List<FloorItem>();
    
    public List<FloorItem> ToAdd = new List<FloorItem>();
    
    public void AddedFloorItem(FloorItem item)
    {
        ToAdd.Add(item);
    }

    public void DestroyedFloorItem(FloorItem item)
    {
        ToRemove.Add(item);
    }
}