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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace AresFramework.GameEngine;

public static class GameServerEngine
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    public static ServiceCollection Services = new ServiceCollection();
    

    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("Settings/world-settings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(Constants.AresFolder + "/world-settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        Constants.Configuration = config;

        var builder = Host.CreateDefaultBuilder(args);
        
        AresServiceCollection.ServiceCollection = new ServiceCollection();
        AresServiceCollection.ServiceCollection.RegisterServices();

        var serviceFactory = new DefaultServiceProviderFactory();
        serviceFactory.CreateBuilder(AresServiceCollection.ServiceCollection);
        builder.UseServiceProviderFactory(serviceFactory);
        AresServiceCollection.ServiceProvider = serviceFactory.CreateServiceProvider(AresServiceCollection.ServiceCollection);
        AresServiceCollection.ServiceCollection.AddLogging(log =>
        {
            log.ClearProviders();
            log.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            log.AddNLog();
        }).BuildServiceProvider();
        
        builder.Build();

        Item i = new Item(4151);

        var def = i.Definition();

        PluginLoader.LoadInternalPlugins();
        PluginLoader.LoadPluginsExternalPlugins();
        
        // Loads and initializes all plugin modules and runs the init function
        PluginLoader.InitializeTypes<IPluginModule>(typeof(IPluginModule), (type, name) =>
        {
            var attribute = name.GetCustomAttributes(typeof(PluginModuleAttribute), true)
                    .FirstOrDefault() as PluginModuleAttribute;

            if (attribute == null)
            {
                Log.Error("Plugins must have a class that inherit both PluginModuleAttribute and IPluginModule " + name.FullName);
                return;
            }
            try
            {
                type.Initialize();
            }
            catch (Exception ex)
            {
                Log.Error($"Error initializing plugin {name.FullName}", ex);
                return;
            }
            Log.Info($"Plugin Initialized: {attribute.Name}, Description: {attribute.Description}, Version: {attribute.Version}, Author: {attribute.Author}, ");
        });

        int pluginLoaders = 0;
        // Runs the IPluginLoader to load specific data / interactions
        PluginLoader.InitializeTypes<IPluginLoader>(typeof(IPluginLoader), (type, name) =>
        {
            type.LoadPlugins();
            pluginLoaders++;
            Log.Debug($"{name.Name} plugin loader loaded");
        });
        Log.Info($"Loaded {pluginLoaders} IPluginLoader loaders");
        
        var npc = new Npc(4, new Position(2, 4, 0));
        var player = new Player("Optimum", new Position(1, 1, 0));
        var npcAgain = new Npc(7, new Position(1, 1, 1));
        var npcAgain2 = new Npc(102, new Position(1, 1, 1));
        
        ClickNpcHandler.ClickNpc(player, npc, "pickpocket");
        ClickNpcHandler.ClickNpc(player, npcAgain, "pickpocket");
        
        player.SkillSet.SetLevel(Skill.Thieving, 3);
        ClickNpcHandler.ClickNpc(player, npcAgain, "pickpocket");
        
        player.SkillSet.SetLevel(Skill.Thieving, 90);
        ClickNpcHandler.ClickNpc(player, npcAgain2, "pickpocket");
        
        
        Log.Fatal("Port " + config["GamePort"]);
        var t = typeof(GameServerEngine).Assembly.GetName().Version;
        Log.Info("Game loaded on the version: " + System.Environment.GetEnvironmentVariable("SERVER_BUILD"));
        //this is a test 
        /*
        GameCache cache = new GameCache("/home/optimum/.ares/Cache/", FileAccess.ReadWrite);
        cache.LoadCache();

        Archive archive = cache.GetArchive(0, 2);
        MemoryStream entry = archive.GetEntry("npc").ByteBuffer;
        */
        
        Console.Read();
    }
    
    
    
}