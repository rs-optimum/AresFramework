namespace AresFramework.Model.Entities;

/// <summary>
/// A permanent attribute map
/// </summary>
public sealed class AttributesMap
{
    private readonly Dictionary<string, int?> _integers = new Dictionary<string, int?>();
    private readonly Dictionary<string, string> _strings = new Dictionary<string, string>();
    private readonly Dictionary<string, double> _doubles = new Dictionary<string, double>();
    private readonly Dictionary<string, float> _floats = new Dictionary<string, float>();
    private readonly Dictionary<string, object> _objects = new Dictionary<string, object>();
    
    public int? GetInt(string name)
    {
        _integers.TryGetValue(name, out var value);
        return value;
    }
    
    /// <summary>
    /// This will get an int or get the default value of int, used for specifically creating new attributes
    /// </summary>
    /// <param name="name"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public int GetIntOrDefault(string name, int defaultValue = 0)
    {
        _integers.TryGetValue(name, out var value);
        if (value == null)
        {
            return defaultValue;
        }
        return value.Value;
    }

    public string? GetString(string name)
    {
        _strings.TryGetValue(name, out var value);
        return value;
    }    
    
    public double? GetDouble(string name)
    {
        _doubles.TryGetValue(name, out var value);
        return value;
    }
    
    public float? GetFloat(string name)
    {
        _floats.TryGetValue(name, out var value);
        return value;
    }
    
    public T? Get<T>(string name) where T : class
    {
        _objects.TryGetValue(name, out var value);
        return value as T;
    }
    
    public void SetInt(string name, int value)
    {
        _integers[name] = value;
    }
    
    public void SetString(string name, string value)
    {
        _strings[name] = value;
    }
    
    public void SetDouble(string name, double value)
    {
        _doubles[name] = value;
    }
    
    public void SetFloat(string name, float value)
    {
        _floats[name] = value;
    }

    public void Set(string name, object value)
    {
        _objects[name] = value;
    }

    /// <summary>
    /// Checks if the attribute maps are empty
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        if (_integers.Count > 0)
        {
            return false;
        }
        if (_strings.Count > 0)
        {
            return false;
        }
        if (_doubles.Count > 0)
        {
            return false;
        }
        if (_floats.Count > 0)
        {
            return false;
        }
        if (_objects.Count > 0)
        {
            return false;
        }
        return true;
    }

}