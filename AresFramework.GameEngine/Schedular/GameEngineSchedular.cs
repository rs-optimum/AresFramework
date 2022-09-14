using AresFramework.GameEngine.Tasks;
using NLog;

namespace AresFramework.GameEngine.Schedular;

/// <summary>
/// This is a game engine schedular
/// </summary>
public static class GameEngineSchedular
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    public static List<ScheduledTask> Tasks { get; set; } = new List<ScheduledTask>();

    private static Timer _timer;

    public static void Initialize()
    {
        _timer = new Timer(TimerExecution, null, 0, 600);
        Log.Info("Scheduled Task Initializer started at 600ms.");
    }
        
    private static void TimerExecution(object? o)
    {
        // Clears the running tasks
        var isRunningTasks = Tasks.Where(e => e.IsRunning).ToList();
        Tasks = isRunningTasks;
        
        foreach (var task in Tasks)
        {
            task.Tick();
        }
    }
}