namespace AresFramework.Model.Entities.FloorItems;

public interface IFloorItemListener
{
    public void AddedFloorItem(FloorItem item);

    public void DestroyedFloorItem(FloorItem item);
}