using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Bcan.MyApp.Data;

namespace Bcan.MyApp.Converters;

public class LogLevelConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // if (targetType != typeof(string))
        //     return new BindingNotification(new InvalidCastException(),
        //         BindingErrorType.Error);

        if (value is not LogLevel level)
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        
        return level switch
        {
            LogLevel.Trace => LogLevel.Trace.ToString(),
            LogLevel.Debug => LogLevel.Debug.ToString(),
            LogLevel.Info => LogLevel.Info.ToString(),
            LogLevel.Warn => LogLevel.Warn.ToString(),
            LogLevel.Error => LogLevel.Error.ToString(),
            LogLevel.Fatal => LogLevel.Fatal.ToString(),
            _ => new BindingNotification(new InvalidCastException(), BindingErrorType.Error)
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string valueStr && targetType.IsAssignableFrom(typeof(LogLevel)))
        {
            if (Enum.TryParse<LogLevel>(valueStr, out var level))
                return level;
        }
        
        return new BindingNotification(new InvalidCastException(), 
            BindingErrorType.Error);
    }
}