using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bcan.MyApp.ViewModels;

public partial class AppSettingVm : ObservableObject
{
    [ObservableProperty] private string _key;
    [ObservableProperty] private object _value;
    [ObservableProperty] private Type _type;
    [ObservableProperty] private string _displayName;

    public AppSettingVm(string key, object value, Type type, string displayName)
    {
        Key = key;
        Value = value;
        Type = type;
        DisplayName = displayName;
    }
}

public partial class AppSettingsPageViewModel : ViewModelBase
{
    private readonly IAppSettingsService _settingsService;
    private ObservableCollection<AppSettingVm> _allSettings; //todo: mark as readonly if init only in ctor

    [ObservableProperty] private string _searchText = string.Empty;
    [ObservableProperty] private ObservableCollection<AppSettingVm> _filteredSettings = new();

#pragma warning disable CS8618, CS9264
    public AppSettingsPageViewModel()
    {
        
    }
#pragma warning restore CS8618, CS9264

    public AppSettingsPageViewModel(IAppSettingsService settingsService)
    {
        _settingsService = settingsService;
        PopulateAllSettings(_settingsService.AllSettings);
        FilteredSettings = new ObservableCollection<AppSettingVm>(_allSettings);
    }

    private void PopulateAllSettings(IEnumerable<AppSetting> settings, bool unregisterFirst = false)
    {
        _allSettings = new ObservableCollection<AppSettingVm>(
            settings
                .Select(s => new AppSettingVm(s.Key, s.Value, s.ValueType, s.DisplayName)));
        
        foreach (var setting in _allSettings)
        {
            if(unregisterFirst)
                setting.PropertyChanged -= OnAppSettingChanged;

            setting.PropertyChanged += OnAppSettingChanged;
        }
    }

    private void OnAppSettingChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(AppSettingVm.Value)) return;
        
        if (sender is AppSettingVm changedSetting)
        {
            _settingsService.SetSetting(changedSetting.Key, changedSetting.Value);
            //TODO: If there is a change refresh view
        }
    }
    
    private void Refresh()
    {
        _allSettings.Clear();
        PopulateAllSettings(_settingsService.AllSettings, true);
        ApplyFilter(); // Re-apply filter
    }


    private void ApplyFilter()
    {
        FilteredSettings.Clear();
        var lowerSearchText = SearchText.ToLowerInvariant();
        var filtered = _allSettings.Where(s =>
            s.DisplayName.Contains(lowerSearchText, StringComparison.InvariantCultureIgnoreCase) ||
            s.Key.Contains(lowerSearchText, StringComparison.InvariantCultureIgnoreCase));

        foreach (var setting in filtered)
            FilteredSettings.Add(setting);
    }
}