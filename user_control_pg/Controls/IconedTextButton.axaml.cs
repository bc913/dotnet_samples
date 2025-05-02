using Avalonia;
using Avalonia.Media;
using System.Windows.Input;


namespace Bcan.Uctrls.Pg.Controls;

public class IconedTextButton : IconedLabel
{
    public static readonly StyledProperty<string> ToolTipTextProperty =
        AvaloniaProperty.Register<IconedButton, string>(nameof(ToolTipText), defaultValue:string.Empty);
    
    // public static readonly StyledProperty<double> ButtonWidthProperty =
    //     AvaloniaProperty.Register<IconedButton, double>(nameof(ButtonWidth), defaultValue: double.NaN);
    //
    // public static readonly StyledProperty<double> ButtonHeightProperty =
    //     AvaloniaProperty.Register<IconedButton, double>(nameof(ButtonHeight), defaultValue: double.NaN);
    
    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<IconedButton, ICommand>(nameof(Command));
    
    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<IconedButton, object>(nameof(CommandParameter), defaultValue: null);
    
    public string ToolTipText
    {
        get => (string)GetValue(ToolTipTextProperty);
        set => SetValue(ToolTipTextProperty, value);
    }
    
    // /// <summary>
    // /// Width of the button
    // /// </summary>
    // public double ButtonWidth
    // {
    //     get => (double)GetValue(ButtonWidthProperty);
    //     set => SetValue(ButtonWidthProperty, value);
    // }
    //
    // /// <summary>
    // /// Height of the button
    // /// </summary>
    // public double ButtonHeight
    // {
    //     get => (double)GetValue(ButtonHeightProperty);
    //     set => SetValue(ButtonHeightProperty, value);
    // }
    //
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
}