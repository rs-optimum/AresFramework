namespace AresFramework.Plugin.Module;

/// <summary>
/// The plugin module attribute to define a plugin
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class PluginModuleAttribute : Attribute
{
    public string Name { get; set; }

    public string Version { get; set; }

    public string Description { get; set; }

    public string Author { get; set; }

    public PluginModuleAttribute(string name, string version, string description, string author)
    {
        Name = name;
        Version = version;
        Description = description;
        Author = author;
    }
}