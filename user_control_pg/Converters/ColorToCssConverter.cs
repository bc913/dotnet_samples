using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Bcan.Uctrls.Pg.Converters;

public class ColorToCssConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string color)
        {
            return $".inner-rect {{ fill: {color}; }}";
        }

        return $".inner-rect {{ fill: blue; }}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public static class BcValueConverters
{
    public static FuncValueConverter<string, string> ColorToCssConverter =>
        new FuncValueConverter<string, string>(color => $".inner-rect {{ fill: {color}; }}");
}