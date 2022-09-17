using AresFramework.Plugin.Ignore.Interacting;

namespace AresFramework.Plugin.Ignore;

/// <summary>
/// This class will specifically be made for plugin ignores for specific plugins
/// </summary>
public class PluginIgnores
{
    
    public static readonly Dictionary<string, PluginIgnores> MappedPluginData = new Dictionary<string, PluginIgnores>();
    
    
    public List<NpcInteractionIgnore> NpcInteractionIgnores = new();
    
    /// <summary>
    /// Checks if a specific npc interaction is ignored
    /// </summary>
    /// <param name="npcId">The npc id</param>
    /// <param name="option">The option we are checking</param>
    /// <returns></returns>
    public bool IsNpcInteractionIgnored(int npcId, string option)
    {
        return NpcInteractionIgnores.Contains(new NpcInteractionIgnore()
        {
            Option = option,
            NpcId = npcId
        });
    }
    
    
}