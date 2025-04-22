using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Bcan.MyApp.Data;

namespace Bcan.MyApp.Contracts;

public interface ILoggerService
{
    void Log(LogLevel level, string message, string? source = null);
    Task LogAsync(LogLevel level, string message, string? source = null);
    ObservableCollection<LogEntry> LogEntries { get; }
    event EventHandler<LogEntry> LogReceived;
}