# Avalonia UI Notes


# Datagrid
https://github.com/endurabyte/FitEdit/blob/main/Ui/FitEdit.Ui/Views/RecordView.axaml
https://github.com/endurabyte/FitEdit/blob/main/Ui/FitEdit.Ui/ViewModels/RecordViewModel.cs

## ContentControl
https://docs.avaloniaui.net/docs/guides/data-binding/how-to-bind-tabs

- Option 1
```axaml
<TabControl Grid.Row="1" Grid.Column="0" ItemsSource="{Binding ShownData}" SelectedIndex="{Binding TabIndex}">
    <TabControl.ItemTemplate>
    <DataTemplate>
        <TextBlock Text="{Binding Name}" FontSize="12"/>
    </DataTemplate>
    </TabControl.ItemTemplate>
    <TabControl.ContentTemplate>
    <DataTemplate DataType="vm:DataGridWrapper">
        <ContentControl Content="{Binding DataGrid}"/>
    </DataTemplate>
    </TabControl.ContentTemplate>
</TabControl>
```

View model
```csharp
 public ObservableCollection<DataGridWrapper> ShownData { get; set; } = new();
```

```csharp
public class DataGridWrapper : ReactiveObject
{
  [Reactive] public DataGrid? DataGrid { get; set; }
}

```

- Option 2 
```xml
<ContentControl Content="{Binding Content}">
    <ContentControl.ContentTemplate>
        <DataTemplate>
        <Grid ColumnDefinitions="Auto,Auto" RowDefinitions="Auto,Auto">
            <TextBlock Grid.Row="0" Grid.Column="0">First Name:</TextBlock>
            <TextBlock Grid.Row="0" Grid.Column="1" Text="{Binding FirstName}"/>
            <TextBlock Grid.Row="1" Grid.Column="0">Last Name:</TextBlock>
            <TextBlock Grid.Row="1" Grid.Column="1" Text="{Binding LastName}"/>
        </Grid>
        </DataTemplate>
    </ContentControl.ContentTemplate>
</ContentControl>
```
## Treeview
- https://github.com/AvaloniaUI/Avalonia/discussions/6365

## ViewModel binding to view
- MainView.axaml
```xml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:vm="clr-namespace:FitEdit.Ui.ViewModels"
             xmlns:views="clr-namespace:FitEdit.Ui.Views"
             mc:Ignorable="d" d:DesignWidth="900" d:DesignHeight="600"
             x:Class="FitEdit.Ui.Views.MainView"
             x:DataType="vm:MainViewModel">
    <Grid Grid.Row="2" x:Name="ChartGrid" ColumnDefinitions="*, Auto, *">
        <views:PlotView Grid.Column="0" DataContext="{Binding Plot}"/>
        <GridSplitter Grid.Column="1" Background="{StaticResource SystemAccentColor}" ResizeDirection="Columns"/>
        <views:MapView Grid.Column="2" DataContext="{Binding Map}" />
    </Grid>
</UserControl>
```

- MainViewModel.cs
```csharp
public class MainViewModel : ViewModelBase, IMainViewModel
{
  public IPlotViewModel Plot { get; }
  public IMapViewModel Map { get; }
}
```

## Icons
- [https://github.com/Projektanker/Icons.Avalonia/tree/main](https://github.com/Projektanker/Icons.Avalonia/tree/main)

## Dialog Service
- https://github.com/grokys/FileDialogMvvm
- https://github.com/AvaloniaUI/Avalonia.Samples
- https://github.com/AvaloniaUI/AvaloniaUI.QuickGuides/tree/main
- https://github.com/mysteryx93/HanumanInstitute.MvvmDialogs/tree/master

## Splash Screen
- https://www.youtube.com/watch?v=-Ii4QmcYQUU
- https://github.com/MammaMiaDev/avaloniaui-the-series/blob/main/src/AvaloniaMiaDev/Views/FluentSplashScreenView.axaml.cs
- https://github.com/kivarsen/AvaloniaSplashScreenDemo/blob/master/AvaloniaSplashScreenDemo/App.axaml.cs
- https://github.com/AvaloniaUI/AvaloniaUI.QuickGuides/tree/main/SplashScreen
- https://github.com/OmerFarukAkyapak/avalonia_guide/blob/main/AvaloniaApp/AvaloniaGuideApp/Views/SplashScreenWindow.axaml

## Repos
- [https://github.com/GPUOpen-Tools/GPU-Reshape.git](https://github.com/GPUOpen-Tools/GPU-Reshape.git)
- [https://github.com/AvaloniaUI/Avalonia.Labs](https://github.com/AvaloniaUI/Avalonia.Labs)
- [https://github.com/AvaloniaCommunity/awesome-avalonia](https://github.com/AvaloniaCommunity/awesome-avalonia)


## References
https://github.com/namigop/FintX.git
https://github.com/LykosAI/StabilityMatrix.git
https://github.com/sajmons/CollimationCircles.git
https://github.com/MammaMiaDev/avaloniaui-the-series.git

https://github.com/timunie/Tims.Avalonia.Samples/blob/main/src/LocalizationSample/App.axaml

https://www.youtube.com/watch?v=XmYKOoGlZWo
https://www.youtube.com/watch?v=xdy9YxuBS5E


https://github.com/AvaloniaUI/AvaloniaUI.QuickGuides/tree/main
https://github.com/timunie/Tims.Avalonia.Samples


