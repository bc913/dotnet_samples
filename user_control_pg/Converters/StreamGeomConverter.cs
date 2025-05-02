using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Bcan.Uctrls.Pg.Converters;

public class StreamGeomConverter : IValueConverter
{
    public static readonly StreamGeomConverter Instance = new();
    //
    // private NavBarIconConverter()
    // {
    // }
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (Application.Current is { } && value is string iconName)
        {
            // if (Application.Current.Styles.TryGetResource(iconName, null, out object? resource))
            // {
            //     return resource is not StreamGeometry ? AvaloniaProperty.UnsetValue : (StreamGeometry)resource;
            // }
            
            if (Application.Current.Resources.TryGetResource(iconName, null, out object? resource))
            {
                return resource is not StreamGeometry ? AvaloniaProperty.UnsetValue : (StreamGeometry)resource;
            }
        }

        return AvaloniaProperty.UnsetValue;
    }
    
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    

    
}