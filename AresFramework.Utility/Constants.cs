using Microsoft.Extensions.Configuration;

namespace AresFramework.Utility;

public static class Constants
{
    public static readonly string AresFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.ares";
    
    public static IConfiguration Configuration { get; set; }
    
}