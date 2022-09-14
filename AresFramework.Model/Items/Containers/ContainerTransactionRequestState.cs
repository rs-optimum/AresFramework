namespace AresFramework.Model.Items.Containers;

public enum ContainerTransactionRequestState
{
    /// <summary>
    /// Successfully executed an action and successfully 
    /// </summary>
    Success,
    
    /// <summary>
    /// The item container caused an overflow when attempting to add, ie above <see cref="int.MaxValue"/>
    /// </summary>
    Overflow,
    
    /// <summary>
    /// There was not enough space to perform the said action
    /// </summary>
    NotEnoughSpace,
    
    /// <summary>
    /// There was not enough items to perform the said action
    /// </summary>
    NotEnoughItems,
    
    /// <summary>
    /// Failed to do an action
    /// </summary>
    Failure
}