using AresFramework.ServiceDependencies.Definitions;
using Microsoft.Extensions.DependencyInjection;

namespace AresFramework.ServiceDependencies;

public static class AresServiceCollection
{
    public static IServiceCollection ServiceCollection { get; set; }
    
    public static IServiceProvider ServiceProvider { get; set; }

    public static IItemDefinitions? ItemDefinitions()
    {
        return ServiceProvider.GetService(typeof(IItemDefinitions)) as IItemDefinitions;
    }
    
}