namespace AresFramework.GameEngine.Tasks;

public class OneExecutionTask : ScheduledTask
{
    
    public OneExecutionTask(int delay) : base(delay, false)
    {
        
    }

    public override void OnExecute()
    {
        Execute();
        Stop();
    }
}