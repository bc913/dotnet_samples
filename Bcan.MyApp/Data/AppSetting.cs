using System;
using System.Text.Json.Serialization;

namespace Bcan.MyApp.Data;

public class AppSetting
{
    #region Fields
    public string Key { get; private set; }
    public object Value { get;  private set; }
    
    [JsonConverter(typeof(AppSettingValueTypeConverter))]
    public Type ValueType { get; private set; }
    
    public string DisplayName { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public bool IsVisble { get; private set; }

    #endregion
    
    public AppSetting(string key, object value, Type valueType, string displayName, string description, string category, bool isVisible = true )
    {
        Key = key;
        Value = value;
        ValueType = valueType;
        DisplayName = displayName;
        Description = description;
        Category = category;
        IsVisble = isVisible;
    }

    public bool TrySetValue(object newValue)
    {
        if (newValue.GetType() == ValueType)
        {
            Value = newValue;
            return true;
        }
        
        // Handle conversion
        if (ValueType == typeof(int) && newValue is string intStr && int.TryParse(intStr, out var intVal))
        {
            Value = intVal;
            return true;
        }
        
        if (ValueType == typeof(bool) && newValue is string boolStr && bool.TryParse(boolStr, out var boolVal))
        {
            Value = boolVal;
            return true;
        }

        if (ValueType == typeof(double) && newValue is string doubleStr &&
            double.TryParse(doubleStr, out var doubleVal))
        {
            Value = doubleVal;
            return true;
        }
        
        return false;
    }
}