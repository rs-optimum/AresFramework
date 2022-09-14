namespace AresFramework.Model.Items.Containers;

/// <summary>
/// A response back to the item container for what happened
/// </summary>
/// <param name="State"></param>
/// <param name="Successful"></param>
/// <param name="Required"></param>
public record ContainerTransactionResult(ContainerTransactionRequestState State, List<Item>? Successful = null)
{
    
    /// <summary>
    /// The state of the container action result
    /// </summary>
    public ContainerTransactionRequestState State { get; set; } = State;
    
    /// <summary>
    /// The items that have successfully been added
    /// </summary>
    public List<Item> Successful { get; } = Successful ?? new List<Item>();
    
    public Item? FirstItem() => Successful.FirstOrDefault();

}