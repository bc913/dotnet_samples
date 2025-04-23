using System.Threading.Tasks;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bcan.MyApp.ViewModels;

public partial class SampleViewModel : ObservableObject
{
    private readonly ILoggerService _logger;

    public SampleViewModel(ILoggerService logger)
    {
        _logger = logger;
        GenerateLogAsyncCommand = new AsyncRelayCommand(GenerateLogAsync);
        
        _ = GenerateInitialLogAsync();
    }

    private async Task GenerateInitialLogAsync()
    {
        await _logger.LogAsync(LogLevel.Info, "SampleViewModel initialized asynchronously.", nameof(SampleViewModel));
        await Task.Delay(2000);
        await _logger.LogAsync(LogLevel.Warn, "Async background warning generated.", nameof(SampleViewModel));
        await Task.Delay(2000);
        await _logger.LogAsync(LogLevel.Error, "Async error occurred.", nameof(SampleViewModel));
    }

    [RelayCommand]
    public void GenerateLog()
    {
        _logger.Log(LogLevel.Debug, "GenerateLog command executed.", nameof(SampleViewModel));
    }

    public IAsyncRelayCommand GenerateLogAsyncCommand { get; }
    
    private async Task<string> GenerateLogAsync()
    {
        await _logger.LogAsync(LogLevel.Trace, "SampleViewModel initialized trace asynchronously.", nameof(SampleViewModel));
        await Task.Delay(3000); // Simulate a web request
        return "Hello world!";
    }

    [RelayCommand]
    public async Task GenerateLogAsync2()
    {
        await _logger.LogAsync(LogLevel.Warn, "GenerateLogAsync2 executed.", nameof(SampleViewModel));
    }
}