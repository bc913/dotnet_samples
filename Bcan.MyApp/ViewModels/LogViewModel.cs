using System;
using System.Collections.ObjectModel;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bcan.MyApp.ViewModels;

public partial class LogViewModel : ObservableObject
{
    private readonly ILoggerService _logger;
    public ObservableCollection<LogEntry> Logs { get; } = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private LogLevel? _filterLevel = null;

    public LogViewModel(ILoggerService logger)
    {
        _logger = logger;

        // logger.LogReceived += (s, e) =>
        // {
        //     App.Current.Dispatcher.Invoke(() =>
        //     {
        //         if (IsMatch(e)) Logs.Add(e);
        //     });
        // };
    }

    private bool IsMatch(LogEntry entry)
    {
        if (!string.IsNullOrWhiteSpace(SearchText) && !entry.Message.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            return false;

        if (FilterLevel.HasValue && entry.Level != FilterLevel.Value)
            return false;

        return true;
    }
}
