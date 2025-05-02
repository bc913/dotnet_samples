using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Bcan.MyApp.Controls;

public partial class ClaudeIconedButton : UserControl
{
    
    #region Dependency Properties

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, string>(nameof(Text), defaultValue:string.Empty);
    
    public static readonly StyledProperty<Geometry> IconDataProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, Geometry>(nameof(IconData));
    
    public static readonly StyledProperty<IBrush> IconBrushProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, IBrush>(nameof(IconBrush), defaultValue:Brushes.White);
    
    public static readonly StyledProperty<IBrush> TextBrushProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, IBrush>(nameof(TextBrush), defaultValue:Brushes.Red);
    
    
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, double>(nameof(IconSize), defaultValue: 24.0);
    
    
    public static readonly StyledProperty<double> TextSizeProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, double>(nameof(TextSize), defaultValue: 12.0);
    
    
    public static readonly StyledProperty<double> ButtonWidthProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, double>(nameof(ButtonWidth), defaultValue: double.NaN);
    
    public static readonly StyledProperty<double> ButtonHeightProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, double>(nameof(ButtonHeight), defaultValue: double.NaN);
    
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, ICommand>(nameof(Command));
    
    
    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<ClaudeIconedButton, object>(nameof(CommandParameter), defaultValue: null);
    

    #endregion

    #region Properties

    /// <summary>
    /// Text to display when hovering over the button
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
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
    /// The brush used for the text color
    /// </summary>
    public IBrush TextBrush
    {
        get => (IBrush)GetValue(TextBrushProperty);
        set => SetValue(TextBrushProperty, value);
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
    /// Font size of the text
    /// </summary>
    public double TextSize
    {
        get => (double)GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
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