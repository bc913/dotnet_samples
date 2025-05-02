using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Bcan.Uctrls.Pg.Controls;

public class IconedButton : TemplatedControl
{
    #region Dependency Properties

    public static readonly StyledProperty<string> ToolTipTextProperty =
        AvaloniaProperty.Register<IconedButton, string>(nameof(ToolTipText), defaultValue:string.Empty);
    
    public static readonly StyledProperty<Geometry> IconDataProperty =
        AvaloniaProperty.Register<IconedButton, Geometry>(nameof(IconData));
    
    public static readonly StyledProperty<IBrush> IconBrushProperty =
        AvaloniaProperty.Register<IconedButton, IBrush>(nameof(IconBrush));
    
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<IconedButton, double>(nameof(IconSize));
    
    public static readonly StyledProperty<double> ButtonWidthProperty =
        AvaloniaProperty.Register<IconedButton, double>(nameof(ButtonWidth), defaultValue: double.NaN);
    
    public static readonly StyledProperty<double> ButtonHeightProperty =
        AvaloniaProperty.Register<IconedButton, double>(nameof(ButtonHeight), defaultValue: double.NaN);
    
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<IconedButton, ICommand>(nameof(Command));
    
    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<IconedButton, object>(nameof(CommandParameter), defaultValue: null);
    

    #endregion

    #region Properties

    /// <summary>
    /// Text to display when hovering over the button
    /// </summary>
    public string ToolTipText
    {
        get => (string)GetValue(ToolTipTextProperty);
        set => SetValue(ToolTipTextProperty, value);
    }

    /// <summary>
    /// The geometry data for the icon
    /// </summary>
    public Geometry IconData
    {
        get => (Geometry)GetValue(IconDataProperty);
        set => SetValue(IconDataProperty, value);
    }

    /// <summary>
    /// The brush used to fill the icon
    /// </summary>
    public IBrush IconBrush
    {
        get => (IBrush)GetValue(IconBrushProperty);
        set => SetValue(IconBrushProperty, value);
    }

    /// <summary>
    /// Size of the icon
    /// </summary>
    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    /// <summary>
    /// Width of the button
    /// </summary>
    public double ButtonWidth
    {
        get => (double)GetValue(ButtonWidthProperty);
        set => SetValue(ButtonWidthProperty, value);
    }

    /// <summary>
    /// Height of the button
    /// </summary>
    public double ButtonHeight
    {
        get => (double)GetValue(ButtonHeightProperty);
        set => SetValue(ButtonHeightProperty, value);
    }

    /// <summary>
    /// Command to execute when the button is clicked
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Parameter to pass to the command
    /// </summary>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion
}