namespace AresFramework.GameEngine.Tasks.Actions;

public class OneExecutionTaskAction : ScheduledTask
{
    private readonly Action<ScheduledTask> _executeAction;
    
    public OneExecutionTaskAction(int delay, Action<ScheduledTask> executeAction) : base(delay, false)
    {
        _executeAction = executeAction;
    }
    
    public override void Execute()
    {
        _executeAction.Invoke(this);
    }

    public override void OnExecute()
    {
        Execute();
        Stop();
    }
}