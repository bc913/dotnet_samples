using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bcan.Uctrls.Pg.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    [ObservableProperty]
    private NavBarViewModel _navBarVm = new();


    [ObservableProperty] private ObservableCollection<IconedButtonVm> _navBarItems = 
    [
        new (){Text = "Osman", Icon="Settings", Fill = "#348ff4"},
        new (){Text = "Hasan", Icon="Download", Fill = "#B00020"}
    ];

    #region CTOR
    public MainWindowViewModel() => NavBarVm = new(
        [new NavBarItemVm("Osman", true), new NavBarItemVm("Necmi", false)],
        [new NavBarItemVm("Settings", false, "FluentIconSgSettingsRegular")]
        );
    #endregion


    [RelayCommand]
    private void BcButtonClicked()
    {
        Console.WriteLine("BcButtonClicked");
    }

    [RelayCommand]
    public void ToggleTheme()
    {
        var actualTheme = Application.Current?.ActualThemeVariant;
        if(actualTheme == ThemeVariant.Dark)
            Application.Current.RequestedThemeVariant = ThemeVariant.Light;
        else if(actualTheme == ThemeVariant.Light)
            Application.Current.RequestedThemeVariant = ThemeVariant.Dark;
        else
            Application.Current.RequestedThemeVariant = ThemeVariant.Default;
    }

    [RelayCommand]
    private void EllipseClicked()
    {
        Console.WriteLine($"{NavBarVm.SelectedItem}");
    }

}
