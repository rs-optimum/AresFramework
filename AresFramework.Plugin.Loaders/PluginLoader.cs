using System.Reflection;
using System.Runtime.Loader;
using AresFramework.Plugin.Module;
using NLog;
using NLog.Fluent;

namespace AresFramework.Plugin.Loaders;

public class PluginLoader : AssemblyLoadContext
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private static readonly HashSet<Assembly> PluginAssemblies = new HashSet<Assembly>();
    private static readonly string PluginPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.ares/Plugins/";
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

    public static void LoadInternalPlugins()
    {
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
        
        foreach (var assembly in assemblies)
        {
            var plugin = Assembly.LoadFrom(assembly);
            PluginAssemblies.Add(plugin);
        }
        
    }
    
    /// <summary>
    /// Loads the external plugins
    /// </summary>
    public static async void LoadPluginsExternalPlugins()
    {
        var files = Directory.GetFiles(PluginPath, "*.dll");
        
        foreach (var file in files)
        {
            var plugin = LoadPlugin(file);
            if (plugin != null)
            {
                PluginAssemblies.Add(plugin);
            }
        }
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
            Log.Warn(ex, $"Error loading {pluginLocation} ");
        }
        return null;
    }

    public static void InitializeTypes<T>(Type classType, Action<T, Type> action)
    {
        foreach (var assembly in PluginAssemblies)
        {
            var types = assembly.GetTypes().Where(e => classType.IsAssignableFrom(e) && (!e.IsInterface && !e.IsAbstract));
            foreach (var type in types)
            {
                try
                {
                    var worldInit = (T) Activator.CreateInstance(type)!;
                    action.Invoke(worldInit, type);
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