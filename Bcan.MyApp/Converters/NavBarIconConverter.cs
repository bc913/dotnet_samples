using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Bcan.MyApp.Converters;

public class NavBarIconConverter : IValueConverter
{
    public static readonly NavBarIconConverter Instance = new();
    //
    // private NavBarIconConverter()
    // {
    // }

    object IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
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

    object IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
