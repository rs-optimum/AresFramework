namespace AresFramework.GameEngine.Tasks;

public abstract class ScheduledTask
{
    public int Delay { get; set; } = 0;
    
    public bool IsRunning { get; set; } = true;
    
    /// <summary>
    /// The amount of ticks remaining until 
    /// </summary>
    private int TicksRemaining { get; set; } = 1;
    
    public ScheduledTask(int delay, bool immediate)
    {
        Delay = delay;
        TicksRemaining = immediate ? 0 : delay;
    }
    
    /// <summary>
    /// Stops the scheduled task
    /// </summary>
    public void Stop() => IsRunning = false;

    public virtual void Execute()
    {
        
    }

    public virtual void OnExecute()
    {
        Execute();
    }

    public void Tick()
    {
        TicksRemaining -= 1;
        if (IsRunning && TicksRemaining <= 0)
        {
            OnExecute();
            TicksRemaining = Delay;
        }
    }

}