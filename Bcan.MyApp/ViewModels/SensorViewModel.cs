using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bcan.MyApp.ViewModels;

public partial class SensorViewModel : ObservableObject
{
    private readonly ILoggerService _loggerService;
    private readonly string _name;
    private readonly CancellationTokenSource _cts = new();

    public SensorViewModel(ILoggerService loggerService, string name)
    {
        _loggerService = loggerService;
        _name = name;
    }
    
    #region Attempt 1
    [RelayCommand]
    private async Task<string> StartAsync()
    {
        var rand = new Random();
        var levels = Enum.GetValues<LogLevel>().ToList();
        while (!_cts.Token.IsCancellationRequested)
        {
            var level = levels[rand.Next(levels.Count)];
            var message = $"[{_name}] generated a {level} log at {DateTime.Now:T}";
            await _loggerService.LogAsync(level, message, _name);
            await Task.Delay(rand.Next(1000, 3000), _cts.Token).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
        }

        return "Simulation Done";
    }
    
    [RelayCommand]
    private async Task StopAsync()
    {
        await _cts.CancelAsync();
        _loggerService.LogAsync(LogLevel.Info, "Sensor stopped", _name).SafeFireAndForget();
    }
    #endregion
    
    #region Attempt 2
    [ObservableProperty] private string _realTimeUpdate = string.Empty;
    [ObservableProperty] private string _realTimeStatus = string.Empty;
    private CancellationTokenSource? _tokenSource;

    [RelayCommand]
    public async Task StartHeavyWorkAsync()
    {
        if (_tokenSource is not null)
        {
            RealTimeStatus = "Already running";
            return;
        }
        
        _tokenSource = new CancellationTokenSource();
        RealTimeStatus = "Starting";

        try
        {
            await DoHeavyWorkAsync(_tokenSource.Token);
            RealTimeStatus = "Done";
        }
        catch (Exception ex)
        {
            RealTimeStatus = ex is OperationCanceledException ? "Cancelled" : ex.Message;
        }
        finally
        {
            _tokenSource.Dispose();
            _tokenSource = null;
        }
    }
    private async Task DoHeavyWorkAsync(CancellationToken token)
    {
        var rand = new Random();
        var levels = Enum.GetValues<LogLevel>().ToList();
        while (!token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
            var level = levels[rand.Next(levels.Count)];
            var message = $"[{_name}] generated a {level} log at {DateTime.Now:T}";
            await _loggerService.LogAsync(level, message, _name);
            await Task
                .Delay(rand.Next(1000, 3000), token);
            //.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
            RealTimeUpdate = $"{rand.Next(1, 6)} samples generated.";
        }
    }

    [RelayCommand]
    public void StopHeavyWork()
    {
        _tokenSource?.Cancel();
    }
    #endregion
    
    #region Simulation
    
    [ObservableProperty]
    private bool _isBusy = false;
    [ObservableProperty]
    private string _status = string.Empty;
    
    [RelayCommand]
    private async Task RunSimulationAsync()
    {
        IsBusy = true;
        var rand = new Random();
        try
        {
            await Task.Delay(rand.Next(2000, 5000));
            Status = "Simulation Completed";
        }
        catch (Exception ex)
        {
            Status = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}