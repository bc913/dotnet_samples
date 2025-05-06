using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Bcan.Uctrls.Pg.Controls;

public partial class NavBar : UserControl
{
    public NavBar()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<double> ButtonHeightProperty = AvaloniaProperty.Register<NavBar, double>(
        nameof(ButtonHeight), 32.0);

    public double ButtonHeight
    {
        get => GetValue(ButtonHeightProperty);
        set => SetValue(ButtonHeightProperty, value);
    }

    public static readonly StyledProperty<double> ButtonWidthProperty = AvaloniaProperty.Register<NavBar, double>(
        nameof(ButtonWidth), 32.0);

    public double ButtonWidth
    {
        get => GetValue(ButtonWidthProperty);
        set => SetValue(ButtonWidthProperty, value);
    }

    public static readonly StyledProperty<double> LabelSizeProperty = AvaloniaProperty.Register<NavBar, double>(
        nameof(LabelSize), 12.0);

    public double LabelSize
    {
        get => GetValue(LabelSizeProperty);
        set => SetValue(LabelSizeProperty, value);
    }
}