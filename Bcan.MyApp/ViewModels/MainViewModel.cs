using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bcan.MyApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private LogViewModel _loggerVm;

    [ObservableProperty] private SampleViewModel _sampleVm;

    public MainViewModel(LogViewModel logViewModel, SampleViewModel sampleViewModel)
    {
        _loggerVm = logViewModel;
        _sampleVm = sampleViewModel;
    }

    [RelayCommand]
    public void GenerateTheLog()
    {
        SampleVm.GenerateLog();
    }
}