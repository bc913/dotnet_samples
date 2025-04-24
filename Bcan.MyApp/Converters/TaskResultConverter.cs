using System;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Bcan.MyApp.Converters;

public class TaskResultConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is not Task<string> task)
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        
        return task.Status == TaskStatus.RanToCompletion ? task.Result : null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return new BindingNotification(new NotImplementedException(), BindingErrorType.Error);
    }
}