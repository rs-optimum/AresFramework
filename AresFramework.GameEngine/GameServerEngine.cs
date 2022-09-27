using AresFramework.Cache;
using AresFramework.Cache.Decoder;
using AresFramework.Model.Entities.Players;
using AresFramework.Model.Handlers;
using AresFramework.Networking;
using AresFramework.Plugin.Loaders;
using AresFramework.ServiceDependency;
using AresFramework.Utility;
using AresFramework.Utility.Extensions;
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
        AresServiceCollection.ServiceCollection.RegisterServices(config);

        var serviceFactory = new DefaultServiceProviderFactory();
        serviceFactory.CreateBuilder(AresServiceCollection.ServiceCollection);
        builder.UseServiceProviderFactory(serviceFactory);
        AresServiceCollection.ServiceProvider = serviceFactory.CreateServiceProvider(AresServiceCollection.ServiceCollection);
        builder.Build();

        //GameCache cache = new GameCache($"{Constants.AresFolder}/Cache/", FileAccess.ReadWrite);
        //cache.LoadCache();

        //var decoder = new NpcDefinitionDecoder(cache);
        //decoder.Decode();
        
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


        Player player = new Player("Optimum", new(1, 1));
        player.Inventory.Items[0] = 4716;
        
        
        DropItemHandler.DropItem(player, 0);
        
        
        // NetworkBootstrap b = new NetworkBootstrap();
        //b.BindAsync(settings!.GamePort);
        
        Console.Read();
    }
    
    
    
}