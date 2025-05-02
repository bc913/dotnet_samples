using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bcan.MyApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private LogViewModel _loggerVm;

    [ObservableProperty] private SampleViewModel _sampleVm;
    
    [ObservableProperty] private SensorViewModel _sensorVm;
    
    public MainViewModel(LogViewModel logViewModel, SampleViewModel sampleViewModel, SensorViewModel sensorViewModel)
    {
        _loggerVm = logViewModel;
        _sampleVm = sampleViewModel;
        _sensorVm = sensorViewModel;

        BottomItems =
        [
            new() { Title = "Osman" , IconName = "HomeIcon"},
            new() { Title = "Necmi", IconName = "SettingsIcon"}
        ];
    }
    
    [ObservableProperty] private ObservableCollection<NavBarItemVm> _bottomItems = [];
    
    [RelayCommand]
    private void NavigateHome()
    {
        Console.WriteLine("Navigating to Home page");
    }

    private void OpenSettings()
    {
        Console.WriteLine("Opening Settings");
    }

    private void AddNewItem()
    {
        Console.WriteLine("Adding new item");
    }
}