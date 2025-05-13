using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;
using Path = System.IO.Path;

namespace Bcan.MyApp.Services;

public class AppSettingsService : IAppSettingsService
{
    #region Fields
    private List<AppSetting> _settings = new();
    private readonly string _settingsFilePath;
    #endregion
    
    #region Events
    // Sync
    public event EventHandler<AppSettingChangedEventArgs>? SettingChanged;
    private void RaiseSettingChanged(string key, object oldValue, object newValue)
    {
        // TODO: GetInvocationList ??
        SettingChanged?.Invoke(this, new AppSettingChangedEventArgs(key, oldValue, newValue));
    }
    // Async
    public event IAppSettingsService.AsyncSettingChangedEventHandler? SettingChangedAsync;

    private async Task RaiseSettingChangedAsync(string key, object oldValue, object newValue)
    {
        // Get all delegates and invoke them
        // This allows multiple subscribers to handle the event async
        var invocationList = SettingChangedAsync?.GetInvocationList();
        var handlerTasks = (
            from IAppSettingsService.AsyncSettingChangedEventHandler handler in invocationList
            select handler(this, new AppSettingChangedEventArgs(key, oldValue, newValue))).ToList();

        await Task.WhenAll(handlerTasks);
    }
    #endregion
    
    #region Ctors

    public AppSettingsService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _settingsFilePath = Path.Combine(appDataPath, "BcanMyApp", "appsettings.json");
        
        Directory.CreateDirectory(Path.GetDirectoryName(_settingsFilePath));
        InitDefaultSettings();
    }

    private void InitDefaultSettings()
    {
        // Load existing settings first
        // if (!LoadSettingsAsync().ConfigureAwait(false).GetAwaiter().GetResult())
        // {
        //     // If loading fails or no settings file, add some default settings
        //     _settings.Add(new AppSetting("Theme", "UI Theme", "Select the application theme (Light/Dark).", "Light", typeof(string), "Appearance"));
        //     _settings.Add(new AppSetting("NotificationsEnabled", "Enable Notifications", "Allow the app to send notifications.", true, typeof(bool), "General"));
        //     _settings.Add(new AppSetting("MaxItemsPerPage", "Items Per Page", "Number of items to display per page.", 20, typeof(int), "Display"));
        //     _settings.Add(new AppSetting("UserEmail", "User Email", "Your registered email address.", string.Empty, typeof(string), "Account"));
        //
        //     // Save these defaults if they were just created
        //     SaveSettingsAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        // }
    }
    #endregion
    
    public bool TryGetSetting<T>(string key, out T value)
    {
        var setting = _settings.FirstOrDefault(s => s.Key == key);
        if (setting is { Value: T typedValue })
        {
            value = typedValue;
            return true;
        }

        value = default;
        return false;
    }

    public void SetSetting<T>(string key, T value)
    {
        var setting = _settings.FirstOrDefault(s => s.Key == key);
        if (setting != null)
        {
            // No change
            if (setting.Value.Equals(value))
                return;
            
            object oldValue = setting.Value;
            if (setting.TrySetValue(value))
            {
                // Fire events (both sync and async)
                RaiseSettingChanged(key, oldValue, setting.Value);
                // Fire and forget for async or await if you need to ensure completion before proceeding
                //TODO: Use the third party library FireAndForget() method
                _ = RaiseSettingChangedAsync(key, oldValue, setting.Value);
                // Optionally, auto save
                _ = SaveSettingsAsync();
            }
            else
            {
                Debug.WriteLine($"Warning: Could not set setting '{key}' due to type mismatch or invalid value: {value}");
            }
        }
        else
        {
            Debug.WriteLine($"Warning: Setting with key '{key}' not found.");
            // Optionally, create setting if it doesn't exist
            // _settings.Add(new SettingEntry(key, $"Dynamic Setting: {key}", "", value, typeof(T), "Dynamic"));
            // RaiseSettingChanged(key, default(T), value);
            // _ = RaiseSettingChangedAsync(key, default(T), value);
            // _ = SaveSettingsAsync();
        }
    }

    public IEnumerable<AppSetting> AllSettings => _settings;

    public AppSetting GetSetting(string key)
    {
        return _settings.First(s => s.Key == key);
    }

    public async Task LoadSettingsAsync()
    {
        if (!File.Exists(_settingsFilePath))
            return;

        try
        {
            var json = await File.ReadAllTextAsync(_settingsFilePath);
            var loadedSettings = JsonSerializer.Deserialize<List<AppSetting>>(json);
            if (loadedSettings == null)
                throw new InvalidOperationException(); //TODO: Better message
            
            _settings.Clear();
            _settings = loadedSettings;
        }
        catch (Exception e)
        {
            Debug.WriteLine($"Error loading settings: {e.Message}");
            throw;
        }
    }

    public async Task SaveSettingsAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_settingsFilePath, json);
            Debug.WriteLine($"Settings saved successfully to {_settingsFilePath}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving settings: {ex.Message}");
        }
    }
}