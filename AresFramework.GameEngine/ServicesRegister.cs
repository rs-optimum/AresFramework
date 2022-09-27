using AresFramework.Cache.Definitions;
using AresFramework.Model.World;
using AresFramework.ServiceDependency;
using AresFramework.ServiceDependency.Definitions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AresFramework.GameEngine;

public static class ServicesRegister
{
    public static void RegisterServices(this IServiceCollection builder, IConfiguration config)
    {
        builder.AddSingleton<IItemDefinitions, ItemDefinitions>();
        builder.AddSingleton<INpcDefinitions, NpcDefinitions>();
        builder.AddSingleton(config.GetSection("ServerSettings").Get<ServerSettings>());
        
        builder.AddOptions();
    }
}