namespace AresFramework.GameEngine.Tasks.Actions;

public delegate void ExecuteDelegate();

/// <summary>
/// A scheduled class as a delegate
/// </summary>
public class ScheduledTaskAction : ScheduledTask
{
    private readonly Action<ScheduledTask> _executeAction;
    
    public ScheduledTaskAction(int delay, bool immediate, Action<ScheduledTask> executeAction) : base(delay, immediate)
    {
        _executeAction = executeAction;
    }
    public override void Execute()
    {
        _executeAction.Invoke(this);
    }
}