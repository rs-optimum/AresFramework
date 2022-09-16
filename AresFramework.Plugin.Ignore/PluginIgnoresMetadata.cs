using System.Reflection;

namespace AresFramework.Plugin.Ignore;

/// <summary>
/// The metadata for the plugin
/// </summary>
public class PluginIgnoresMetadata
{
    
    /// <summary>
    /// The plugin assembly
    /// </summary>
    public Assembly PluginAssembly { get; set; }

    /// <summary>
    /// The folder this assembly belongs too
    /// </summary>
    public string? Folder { get; set; }
    
    /// <summary>
    /// The ignores file
    /// </summary>
    public PluginIgnores? Ignores { get; set; }
}