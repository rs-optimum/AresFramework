namespace AresFramework.Model.Items.Containers.Transactions;

/// <summary>
/// A container transaction, adding / removing items
/// </summary>
/// <param name="State">The state of the transaction</param>
/// <param name="Successful">If the transaction was successful or not</param>
/// <param name="Required">The required transaction</param>
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
    
    public Item? FirstSuccessfulItem() => Successful.FirstOrDefault();

}