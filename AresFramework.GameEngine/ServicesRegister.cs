using AresFramework.Cache.Items;
using AresFramework.ServiceDependencies.Definitions;
using Microsoft.Extensions.DependencyInjection;

namespace AresFramework.GameEngine;

public static class ServicesRegister
{
    public static void RegisterServices(this IServiceCollection builder)
    {
        builder.AddSingleton<IItemDefinitions, ItemDefinitions>();
    }
}