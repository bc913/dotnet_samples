using Avalonia;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Mixins;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Bcan.Uctrls.Pg.Controls;

public enum LabelPosition
{
    Bottom, // Default
    Top,
    Left,
    Right
}

[PseudoClasses(":pointerover", ":selected")]
public class IconedLabel : TemplatedControl
{
    static IconedLabel()
    {
        SelectableMixin.Attach<IconedLabel>(IsSelectedProperty);
    }
    
    #region Icon
    public static readonly StyledProperty<Geometry> IconDataProperty =
        AvaloniaProperty.Register<IconedLabel, Geometry>(nameof(IconData));
    
    /// <summary>
    /// The geometry data for the icon
    /// </summary>
    public Geometry IconData
    {
        get => (Geometry)GetValue(IconDataProperty);
        set => SetValue(IconDataProperty, value);
    }
    
    public static readonly StyledProperty<IBrush> IconBrushProperty =
        AvaloniaProperty.Register<IconedLabel, IBrush>(nameof(IconBrush));
    
    /// <summary>
    /// The brush used to fill the icon
    /// </summary>
    public IBrush IconBrush
    {
        get => (IBrush)GetValue(IconBrushProperty);
        set => SetValue(IconBrushProperty, value);
    }
    
    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<IconedLabel, double>(nameof(IconSize));

    /// <summary>
    /// Size of the icon
    /// </summary>
    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    #endregion
    
    #region Label
    
    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<IconedLabel, string>(nameof(Label), string.Empty);
    
    public static readonly StyledProperty<IBrush> LabelBrushProperty =
        AvaloniaProperty.Register<IconedLabel, IBrush>(nameof(LabelBrush));
    
    public static readonly StyledProperty<double> LabelSizeProperty =
        AvaloniaProperty.Register<IconedLabel, double>(nameof(LabelSize), defaultValue: 12.0);
    
    public static readonly StyledProperty<bool> IsSelectedProperty =
        SelectingItemsControl.IsSelectedProperty.AddOwner<IconedLabel>();

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
    
    /// <summary>
    /// The brush used for the text color
    /// </summary>
    public IBrush LabelBrush
    {
        get => (IBrush)GetValue(LabelBrushProperty);
        set => SetValue(LabelBrushProperty, value);
    }
    
    /// <summary>
    /// Font size of the text
    /// </summary>
    public double LabelSize
    {
        get => (double)GetValue(LabelSizeProperty);
        set => SetValue(LabelSizeProperty, value);
    }
    
    public static readonly StyledProperty<LabelPosition> TextPositionProperty =
        AvaloniaProperty.Register<IconedLabel, LabelPosition>(
            nameof(TextPosition),
            defaultValue: LabelPosition.Bottom); // Default position is Bottom

    public LabelPosition TextPosition
    {
        get => GetValue(TextPositionProperty);
        set => SetValue(TextPositionProperty, value);
    }
    
    #endregion
}