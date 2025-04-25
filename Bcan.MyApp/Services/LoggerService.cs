using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Bcan.MyApp.Contracts;
using Bcan.MyApp.Data;

namespace Bcan.MyApp.Services;

public sealed class LoggerService : ILoggerService, IAsyncDisposable, IDisposable
{
    #region Fields
    
    private readonly string _logFilePath;
    private readonly LogLevel _minimumLogLevel;
    private StreamWriter _streamWriter;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1); // Control access to the file
    private bool _disposed = false;
    #endregion

    public LoggerService(LogLevel minimumLogLevel = LogLevel.Info)
    {
        _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            $"{DateTime.Now:yyyyMMddTHHmmss}_myapplog.txt");
        _minimumLogLevel = minimumLogLevel;
        //EnsureLogDirectoryExists();
        //_streamWriter = new StreamWriter(_logFilePath, true); // Append mode
        _streamWriter = new StreamWriter(new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read));
        _streamWriter.AutoFlush = true; // Consider buffering for performance in high-volume scenarios
    }
    
    #region Properties
    public ObservableCollection<LogEntry> LogEntries { get; } = new();
    public event EventHandler<LogEntry>? LogReceived;

    // TODO: Fetch this from the ISettings
    public uint MaxLogEntries { get; } = 20;
    
    #endregion
    
    
    public void Log(LogLevel level, string message, string? source = null)
    {
        var entry = new LogEntry { Level = level, Message = message, Source = source };
        WriteToFileAsync(entry).SafeFireAndForget();
        //WriteToFileAsync(entry).ConfigureAwait(false).GetAwaiter().GetResult();
        //LogEntries.Add(entry);
        LogReceived?.Invoke(this, entry);
    }

    public async Task LogAsync(LogLevel level, string message, string? source = null)
    {
        await Task.Run(() => Log(level, message, source));
    }
    
    #region Private
    private void EnsureLogDirectoryExists()
    {
        var directory = Path.GetDirectoryName(_logFilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }
    }
    
    private async Task WriteToFileAsync(LogEntry entry)
    {
        // if (entry.Level < _minimumLogLevel)
        //     return;
        
        // TODO: Investigate ConfigureAwait
        await _semaphore.WaitAsync();//.ConfigureAwait(false);
        try
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            await _streamWriter.WriteLineAsync($"" +
                                               $"{entry.Timestamp:T} " +
                                               $"[{entry.Level}] | {entry.Message}");//.ConfigureAwait(false);
            
            // await _streamWriter.FlushAsync(); if AutoFlush is off
        }
        finally
        {
            _semaphore.Release();
        }
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _semaphore.Wait();
            try
            {
                if (!_disposed)
                {
                    _streamWriter.Flush();
                    _streamWriter.Dispose();
                    _disposed = true;
                }
            }
            finally
            {
                _semaphore.Release();
                _semaphore.Dispose();
                _streamWriter = null;
            }
        }
    }
    
    #endregion
    
    #region IAsyncDisposable
    //https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync
    public async ValueTask DisposeAsync()
    {
        // Perform async cleanup.
        await DisposeAsyncCore(); //.ConfigureAwait(false);
        // Dispose of managed resources synchronously now that async cleanup is done.
        Dispose(false);
        // Suppress finalization since resources have been cleaned up.
        GC.SuppressFinalize(this);
    }

    private async ValueTask DisposeAsyncCore()
    {
        if (_disposed)
            return;
        
        await _semaphore.WaitAsync(); // .ConfigureAwait(false);
        try
        {
            if (!_disposed)
            {
                await _streamWriter.FlushAsync(); //.ConfigureAwait(false);
                await _streamWriter.DisposeAsync(); //.ConfigureAwait(false);
                _disposed = true;
            }
        }
        finally
        {
            _semaphore.Release();
            _semaphore.Dispose();
            _streamWriter = null;
        }
    }
    #endregion
    
    ~LoggerService()
    {
        Dispose(false);
    }
}
