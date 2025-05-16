# Avalonia PG

## [DataTemplate vs ControlTemplate](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/styles-templates-overview?view=netdesktop-9.0#controltemplates)
- `ControlTemplate` defines the appearance of the control while `DataTemplate` determines the appearance of the data.

### DataTemplate
- Can be defined as resource
- [https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/data-templating-overview?view=netframeworkdesktop-4.8]
## Styles
- Can be defined as 
1. resources(using x:Key) in a seperate file
2. window, usercontrol or control resources or styles
3. part of ControlTemplate (alongside with TemplatedControl)

## [ControlThemes](https://docs.avaloniaui.net/docs/basics/user-interface/styling/control-themes)
- Built upon Styles
- It is stored under Resources
- Control themes are assigned to a control by setting the Theme property, usually using the {StaticResource} markup extension
- It completely overrides the styles and properties of the `TargetType`


### ItemsControl
- ItemsControl can not select items. It can only present.
```xaml
<ItemsControl ItemsSource="{Binding Items}">
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
            <ReversibleStackPanel ReverseOrder="True" />
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>

    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding}" />
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>


<ItemsControl ItemsSource="{Binding Items}">
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
            <ReversibleStackPanel ReverseOrder="True" />
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>
    <ItemsControl.DataTemplates>
        <!--DataTemplate inside of DataTemplates must have a DataType set. Set DataType property or use ItemTemplate with single template instead.-->
        <DataTemplate DataType="sys:String">
            <TextBlock Text="{Binding}"/>
        </DataTemplate>
    </ItemsControl.DataTemplates>
</ItemsControl>
```

```
                                <Button.Styles>
                                    <Style Selector="Button:pointerover /template/ ContentPresenter">
                                        <Setter Property="Background" Value="{DynamicResource AppPrimaryColor}" />
                                        <Setter Property="Cursor" Value="Hand" />
                                        <Setter Property="Opacity" Value="0.8"/>
                                    </Style>

                                    <Style Selector="Button:pressed /template/ ContentPresenter">
                                        <Setter Property="RenderTransform" Value="scale(0.98)" />
                                    </Style>
                                    
                                </Button.Styles>
```



## Using DataTemplates for collection of different types or values
1. Using IValueConverter and DataTemplates

```csharp
public class AppSettingTemplateSelector : IValueConverter
{
    public IDataTemplate StringTemplate { get; set; }
    public IDataTemplate BooleanTemplate { get; set; }
    public IDataTemplate IntTemplate { get; set; }
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is AppSettingVm appSetting)
        {
            if (appSetting.Value is string && StringTemplate is not null)
                return StringTemplate;
        
            if (appSetting.Value is int && IntTemplate is not null)
                return IntTemplate;
        
            if(appSetting.Value is bool && BooleanTemplate is not null)
                return BooleanTemplate;
        }
        
        
        return new BindingNotification(new InvalidCastException(), 
            BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return new BindingNotification(new InvalidCastException(), 
            BindingErrorType.Error);
    }
}
```

```xaml
<UserControl.Resources>
        
        <DataTemplate x:Key="AppStringSettingTemplate" x:DataType="vm:AppSettingVm">
            <TextBox Text="{Binding Value}"></TextBox>
        </DataTemplate>
        
        <DataTemplate x:Key="AppBoolSettingTemplate" x:DataType="vm:AppSettingVm">
            <CheckBox IsChecked="{Binding Value}"></CheckBox>
        </DataTemplate>
        
        <DataTemplate x:Key="AppIntSettingTemplate" x:DataType="vm:AppSettingVm">
            <NumericUpDown Value="{Binding Value}"></NumericUpDown>
        </DataTemplate>
        
        <conv:AppSettingTemplateSelector x:Key="AppSettingTemplateSelector"
                                         StringTemplate="{StaticResource AppStringSettingTemplate}"
                                         BooleanTemplate="{StaticResource AppBoolSettingTemplate}"
                                         IntTemplate="{StaticResource AppIntSettingTemplate}"/>
    </UserControl.Resources>
    
    <ItemsControl ItemsSource="{Binding FilteredSettings}">
        <ItemsControl.ItemTemplate>
            <DataTemplate DataType="vm:AppSettingVm">
                <StackPanel Orientation="Vertical">
                    <TextBlock Text="{Binding DisplayName}"></TextBlock>
                    <ContentControl Margin="10 0 0 0"
                                    Content="{Binding }"
                                    ContentTemplate="{Binding ., Converter={StaticResource AppSettingTemplateSelector}}"/>    
                </StackPanel>
                
            </DataTemplate>
        </ItemsControl.ItemTemplate>
    </ItemsControl>
```

2. Using IDataTemplate

```csharp
public class AppSettingDataTemplateSelector : IDataTemplate
{
    public IDataTemplate? TextTemplate { get; set; }
    public IDataTemplate? BooleanTemplate { get; set; }
    public IDataTemplate? IntTemplate { get; set; }
    
    public Control? Build(object? param)
    {
        if (param is not AppSettingVm appSetting)
            throw new NotSupportedException();
        
        if(appSetting.Type == typeof(string))
            return TextTemplate?.Build(param ?? throw new NullReferenceException());
        
        if(appSetting.Type == typeof(bool))
            return BooleanTemplate?.Build(param ?? throw new NullReferenceException());
        
        if(appSetting.Type == typeof(int))
            return IntTemplate?.Build(param ?? throw new NullReferenceException());
        
        throw new NotSupportedException();
    }

    public bool Match(object? data)
    {
        return data is AppSettingVm;
    }
}
```

```xaml
<UserControl.Resources>

    <DataTemplate x:Key="AppStringSettingTemplate" x:DataType="vm:AppSettingVm">
        <TextBox Text="{Binding Value}"></TextBox>
    </DataTemplate>
    
    <DataTemplate x:Key="AppBoolSettingTemplate" x:DataType="vm:AppSettingVm">
        <CheckBox IsChecked="{Binding Value}"></CheckBox>
    </DataTemplate>
    
    <DataTemplate x:Key="AppIntSettingTemplate" x:DataType="vm:AppSettingVm">
        <NumericUpDown Value="{Binding Value}"></NumericUpDown>
    </DataTemplate>

</UserControl.Resources>

<ItemsControl ItemsSource="{Binding FilteredSettings}">
    <ItemsControl.DataTemplates>
        <conv:AppSettingDataTemplateSelector
            TextTemplate="{StaticResource AppStringSettingTemplate}"
            BooleanTemplate="{StaticResource AppBoolSettingTemplate}"
            IntTemplate="{StaticResource AppIntSettingTemplate}"></conv:AppSettingDataTemplateSelector>
    </ItemsControl.DataTemplates>
</ItemsControl>
```

or
```xaml
<ItemsControl ItemsSource="{Binding FilteredSettings}">
    <ItemsControl.Resources>
        <DataTemplate x:Key="AppStringSettingDataTemplate" x:DataType="vm:AppSettingVm">
            <TextBox Text="{Binding Value}"></TextBox>
        </DataTemplate>

        <DataTemplate x:Key="AppBoolSettingDataTemplate" x:DataType="vm:AppSettingVm">
            <CheckBox IsChecked="{Binding Value}"></CheckBox>
        </DataTemplate>

        <DataTemplate x:Key="AppIntSettingDataTemplate" x:DataType="vm:AppSettingVm">
            <NumericUpDown Value="{Binding Value}"></NumericUpDown>
        </DataTemplate>
    </ItemsControl.Resources>
    <ItemsControl.DataTemplates>
        <conv:AppSettingDataTemplateSelector
            TextTemplate="{StaticResource AppStringSettingDataTemplate}"
            BooleanTemplate="{StaticResource AppBoolSettingDataTemplate}"
            IntTemplate="{StaticResource AppIntSettingDataTemplate}"></conv:AppSettingDataTemplateSelector>
    </ItemsControl.DataTemplates>
</ItemsControl>
```

## Async Initialization
- https://www.reddit.com/r/AvaloniaUI/comments/1h0gjq5/async_initialization/
- https://github.com/wieslawsoltes/Xaml.Behaviors