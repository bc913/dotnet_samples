using CommunityToolkit.Mvvm.ComponentModel;

namespace Bcan.MyApp.ViewModels;

public partial class MainViewModel: ObservableObject
{
    public LogViewModel LogViewModel { get; }
    public SampleViewModel SampleViewModel { get; }

    public MainViewModel(LogViewModel logViewModel, SampleViewModel sampleViewModel)
    {
        LogViewModel = logViewModel;
        SampleViewModel = sampleViewModel;
    }
}