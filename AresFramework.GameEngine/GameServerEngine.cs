using System.Runtime.CompilerServices;
using AresFramework.Cache;
using AresFramework.Cache.Archives;
using AresFramework.GameEngine.Schedular;
using AresFramework.GameEngine.Tasks;
using AresFramework.GameEngine.Tasks.Actions;
using AresFramework.Model.Entity;
using AresFramework.Model.Entity.Action;
using AresFramework.Model.Entity.Skills;
using AresFramework.Model.Items;
using AresFramework.Model.World;
using AresFramework.Plugin.Loaders;
using AresFramework.Plugin.Module;
using AresFramework.ServiceDependencies;
using AresFramework.Utilities;
using AresFramework.Utilities.Extensions;
using ConfigurationSubstitution;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Extensions.Logging;

namespace AresFramework.GameEngine;

public static class GameServerEngine
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("Settings/settings.json")
            .AddJsonFile($"Settings/settings.{Environment.GetEnvironmentVariable("GAME_ENV")}.json", optional: true, reloadOnChange: true)
            .AddJsonFile(Constants.AresFolder + "/settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        
        LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog")); // Configure NLog
        Constants.Configuration = config;

        var startTick = DateTime.Now.Ticks;
        Log.Info("Initializing Game Server");
        
        var builder = Host.CreateDefaultBuilder(args);
        
        AresServiceCollection.ServiceCollection = new ServiceCollection();
        AresServiceCollection.ServiceCollection.RegisterServices();

        var serviceFactory = new DefaultServiceProviderFactory();
        serviceFactory.CreateBuilder(AresServiceCollection.ServiceCollection);
        builder.UseServiceProviderFactory(serviceFactory);
        AresServiceCollection.ServiceProvider = serviceFactory.CreateServiceProvider(AresServiceCollection.ServiceCollection);
        builder.Build();

        Parallel.Invoke(() => PluginLoader.LoadInternalPlugins(), () => PluginLoader.LoadPluginsExternalPlugins());
        
        Log.Info("Loaded plugin assemblies");
        
        PluginLoader.InitializePlugins();
        

        int pluginLoaders = 0;
        // Runs the IPluginLoader to load specific data / interactions
        PluginLoader.InitializeTypes<IPluginLoader>(typeof(IPluginLoader), (type, name, assembly) =>
        {
            type.LoadPlugins();
            pluginLoaders++;
            Log.Debug($"{name.Name} loaded");
        });
        
        Log.Info($"Loaded {pluginLoaders} {nameof(IPluginLoader)} loaders");
        Log.Info($"Game initialized on the port {config["GamePort"]}, " +
                 $"Version: {Environment.GetEnvironmentVariable("SERVER_BUILD")}, " +
                 $"Environment: {Environment.GetEnvironmentVariable("GAME_ENV")}");

        var finishedTick = DateTime.Now.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(finishedTick - startTick);
        Log.Info($"Game Engine took {elapsedSpan.TotalMilliseconds.FormatWithCommas()} ms to start");
        
        Console.Read();
    }
    
    
    
}