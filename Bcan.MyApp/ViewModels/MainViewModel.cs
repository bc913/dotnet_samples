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
    }
    
    
    
}