using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using AresFramework.Plugin.Ignore;
using AresFramework.Plugin.Module;
using AresFramework.Utility;
using NLog;
using NLog.Fluent;
using YamlDotNet.Serialization.NamingConventions;

namespace AresFramework.Plugin.Loaders;

public class PluginLoader : AssemblyLoadContext
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// This will contain the folder that an assembly has came from,
    /// also this will output all of the assembly information
    /// </summary>
    public static readonly HashSet<Assembly> PluginAssemblies = new HashSet<Assembly>();
    private static readonly string PluginPath = Constants.AresFolder + "/Plugins/";
    private readonly AssemblyDependencyResolver _resolver;
    public PluginLoader(string pluginPath)
    {
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        if (assemblyPath != null)
        {
            return LoadFromAssemblyPath(assemblyPath);
        }

        return null;
    }
    
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        if (libraryPath != null)
        {
            return LoadUnmanagedDllFromPath(libraryPath);
        }

        return IntPtr.Zero;
    }

    public static Task LoadInternalPlugins()
    {
        Log.Debug("Loading internal plugins");
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
        foreach (var assembly in assemblies)
        {
            var plugin = Assembly.LoadFrom(assembly);
            PluginAssemblies.Add(plugin);
        }

        Log.Debug("Loaded internal plugins");
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Loads the external plugins
    /// </summary>
    public static Task LoadPluginsExternalPlugins()
    {
        Log.Debug("Loading external plugins");
        var directories = Directory.GetDirectories(PluginPath);
        foreach (var directory in directories)
        {
            var files = Directory.GetFiles(directory, "*.dll");
            Log.Debug("A list of external plugins: " + string.Join(", ", files));
            LoadAssemblies(files, directory);
        }


        Log.Debug("Loaded external plugins");
        return Task.CompletedTask;
    }

    public static void LoadAssemblies(string[] assemblies, string? directory = null)
    {
        foreach (var file in assemblies)
        {
            var plugin = LoadPlugin(file);
            if (plugin != null)
            {
                if (directory != null && plugin.GetName().FullName.Contains("AresFramework.Plugins."))
                {
                    var ignores = ParseIgnores(directory);
                    if (ignores != null)
                    {
                        PluginIgnores.MappedPluginData.Add(plugin.GetName().FullName, ignores);
                    }
                }
                
                PluginAssemblies.Add(plugin);
            }
        }  
    }

    public static PluginIgnores? ParseIgnores(string directory)
    {
        var file = Path.Combine(directory, "ignores.yml");
        if (!File.Exists(file))
        {
            return null;
        }
        try
        {
            var content = Encoding.UTF8.GetString(File.ReadAllBytes(file));
            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var ignores = deserializer.Deserialize<PluginIgnores>(content);
            return ignores;
        }
        catch(Exception ex)
        {
            return null;
        }
       
    }
    
    /// <summary>
    /// Initializes the plugins
    /// </summary>
    public static void InitializePlugins()
    {
        var initializedModuleTaskList = new List<Task>();
        // Loads and initializes all plugin modules and runs the init function
        InitializeTypes<IPluginModule>(typeof(IPluginModule), (type, name, assembly) =>
        {
            try
            {
                var attribute = name.GetCustomAttributes(typeof(PluginModuleAttribute), true)
                    .FirstOrDefault() as PluginModuleAttribute;

                if (attribute == null)
                {
                    Log.Error($"Plugins must have a class that inherit both {nameof(PluginModuleAttribute)} and {nameof(IPluginModule)}: " + name.FullName);
                    return;
                }
                Log.Debug($"Initiating Plugin: {attribute.Name}, Description: {attribute.Description}, Version: {attribute.Version}, Author: {attribute.Author}");
                initializedModuleTaskList.Add(Task.Run(type.Initialize)
                    .ContinueWith((t) =>
                    {
                        if (t.IsCompleted)
                        {
                            Log.Info($"Plugin Initialized: {attribute.Name}, Description: {attribute.Description}, Version: {attribute.Version}, Author: {attribute.Author}");
                        }
                        if (t.IsFaulted)
                        {
                            Log.Error(t.Exception, $"Error initializing plugin {name.FullName}");
                        }
                    }));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error loading assembly: " + name.Name);
            }
        });
        
        Task.WaitAll(initializedModuleTaskList.ToArray());
    }
    
    private static Assembly? LoadPlugin(string pluginLocation)
    {
        var plugin = new PluginLoader(pluginLocation);
        try
        {
            var loadContext = plugin.LoadFromAssemblyName(AssemblyName.GetAssemblyName(pluginLocation));
            return loadContext;
        }
        catch(Exception ex)
        {
            Log.Error(ex, $"Error loading {pluginLocation}");
        }
        return null;
    }

    public static void InitializeTypes<T>(Type classType, Action<T, Type, Assembly?> action)
    {
        foreach (var assembly in PluginAssemblies)
        {
            var types = assembly.GetTypes().Where(e => classType.IsAssignableFrom(e) && (!e.IsInterface && !e.IsAbstract));
            foreach (var type in types)
            {
                try
                {
                    var worldInit = (T) Activator.CreateInstance(type)!;
                    action.Invoke(worldInit, type, assembly);
                }
                catch(Exception ex)
                {
                    // ignored
                    Log.Debug(ex, $"Error initializing type {type.Name} as {classType.Name}");
                }
            }
        }
    }

}
