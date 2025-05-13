using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bcan.MyApp.Data;

namespace Bcan.MyApp.Contracts;

public class AppSettingChangedEventArgs(string key, object oldValue, object newValue) : EventArgs
{
    public string Key { get; } = key;
    public object OldValue { get; } = oldValue;
    public object NewValue { get; } = newValue;
}

public interface IAppSettingsService
{
    // Sync Event
    event EventHandler<AppSettingChangedEventArgs>? SettingChanged;
    // Async Event
    delegate Task AsyncSettingChangedEventHandler(object sender, AppSettingChangedEventArgs e);
    event AsyncSettingChangedEventHandler? SettingChangedAsync;
    
    // Settings management
    bool TryGetSetting<T>(string key, out T value);
    void SetSetting<T>(string key, T value);
    IEnumerable<AppSetting> AllSettings { get; }
    AppSetting GetSetting(string key);
    
    // Persistence
    Task LoadSettingsAsync();
    Task SaveSettingsAsync();
}