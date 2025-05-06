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