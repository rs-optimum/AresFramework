namespace AresFramework.ServiceDependency;

/// <summary>
/// The server settings for the whole world
/// </summary>
public class ServerSettings
{
    
    public string ServerName { get; set; }
    
    public int GamePort { get; set; }

    /// <summary>
    /// This will enable better logging for item id's etc
    /// </summary>
    public bool EnableGameDebug { get; set; }


}