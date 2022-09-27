namespace AresFramework.Utility.Extensions;

/// <summary>
/// Contains a bunch of number extensions
/// </summary>
public static class NumberExtensions
{
    
    public static string FormatWithCommas(this int number)
    {
        return $"{number:n0}";
    }
    
    public static string FormatWithCommas(this long number)
    {
        return $"{number:n0}";
    }
    
    public static string FormatWithCommas(this double number)
    {
        return $"{number:n0}";
    }

        
    public static string FormatWithCommasWithDecimals(this int number)
    {
        return $"{number:n}";
    }
    
    public static string FormatWithCommasWithDecimals(this long number)
    {
        return $"{number:n}";
    }
    
}