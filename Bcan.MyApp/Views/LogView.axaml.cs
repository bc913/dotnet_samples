using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Bcan.MyApp.Views;

public partial class LogView : UserControl
{
    public LogView()
    {
        InitializeComponent();
    }

    private void LogView_Unloaded(object sender, RoutedEventArgs e)
    {
        (this.DataContext as IDisposable)?.Dispose();
    }
}