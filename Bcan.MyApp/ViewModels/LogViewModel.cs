using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bcan.MyApp.ViewModels;

public partial class LogViewModel : ObservableObject
{
    private readonly ILoggerService _logger;
    public ObservableCollection<LogEntry> Logs { get; } = new();
    public ObservableCollection<LogEntry> FilteredLogs { get; } = new();
    

    [ObservableProperty]
    private string? _searchText;

    [ObservableProperty]
    private string? _filterLevel;
    
    [ObservableProperty]
    private IEnumerable<string> _logLevels = Enum.GetNames<LogLevel>();

#pragma warning disable CS8618, CS9264
    public LogViewModel()
    {
        
    }
#pragma warning restore CS8618, CS9264
    public LogViewModel(ILoggerService logger)
    {
        _logger = logger;

        logger.LogReceived += (s, e) =>
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                //if (IsMatch(e))
                if(Logs.Count >= logger.MaxLogEntries)
                    Logs.RemoveAt(0);
                
                Logs.Add(e);
                ApplyFilter();
            });
        };
        
        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(FilterLevel) || e.PropertyName == nameof(SearchText))
            {
                ApplyFilter();
            }
        };
    }

    private bool IsMatch(LogEntry entry)
    {
        if (!string.IsNullOrWhiteSpace(SearchText) && !entry.Message.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            return false;

        // if (FilterLevel.HasValue && entry.Level != FilterLevel.Value)
        //     return false;

        if (!Enum.TryParse<LogLevel>(FilterLevel, out var filterLev))
            return false;

        if (filterLev != entry.Level)
            return false;
        
        return true;
    }
    
    private void ApplyFilter()
    {
        FilteredLogs.Clear();
        var filtered = Logs.AsEnumerable();

        // if (FilterLevel is not null)
        //     filtered = filtered.Where(log => log.Level == FilterLevel);
        
        if (!string.IsNullOrWhiteSpace(FilterLevel) &&
            Enum.TryParse<LogLevel>(FilterLevel, out var level) && level != LogLevel.All)
        {
            filtered = filtered.Where(log => log.Level == level);
        }
        
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(log =>
                log.Message.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        foreach (var entry in filtered)
        {
            FilteredLogs.Add(entry);
        }
    }
    
    [RelayCommand]
    public void ClearLogs()
    {
        Logs.Clear();
        FilteredLogs.Clear();
    }
}
