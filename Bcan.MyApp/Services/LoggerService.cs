using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;

namespace Bcan.MyApp.Services;

public class LoggerService : ILoggerService
{
    public ObservableCollection<LogEntry> LogEntries { get; } = new();
    public event EventHandler<LogEntry>? LogReceived;

    // TODO: Fetch this from the ISettings
    public uint MaxLogEntries { get; } = 20;

    public void Log(LogLevel level, string message, string? source = null)
    {
        var entry = new LogEntry { Level = level, Message = message, Source = source };
        LogEntries.Add(entry);
        LogReceived?.Invoke(this, entry);
    }

    public async Task LogAsync(LogLevel level, string message, string? source = null)
    {
        await Task.Run(() => Log(level, message, source));
    }
}
