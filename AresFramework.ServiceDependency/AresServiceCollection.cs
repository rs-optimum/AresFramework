using AresFramework.ServiceDependency.Definitions;
using Microsoft.Extensions.DependencyInjection;

namespace AresFramework.ServiceDependency;

public static class AresServiceCollection
{
    public static IServiceCollection ServiceCollection { get; set; }
    
    public static IServiceProvider ServiceProvider { get; set; }

    public static IItemDefinitions? ItemDefinitions => ServiceProvider.GetService(typeof(IItemDefinitions)) as IItemDefinitions;
    public static INpcDefinitions? NpcDefinitions => ServiceProvider.GetService(typeof(INpcDefinitions)) as INpcDefinitions;
    public static ServerSettings? ServerSettings => ServiceProvider.GetService(typeof(ServerSettings)) as ServerSettings;

}