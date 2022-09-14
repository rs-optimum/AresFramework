namespace AresFramework.Plugin.Module;

/// <summary>
/// Represents the basis of a plugin module
/// </summary>
public interface IPluginModule
{
    /// <summary>
    /// Initializes the plugin
    /// </summary>
    Task Initialize();
    
}