using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bcan.Uctrls.Pg.ViewModels;

public class NavBarItemVm(string text, bool isSelected, string? iconName = null)
{
    public string Text {get; set;} = text;
    public bool IsSelected { get; set; } = isSelected;

    public string? IconName { get; set; } = iconName;

    public override string ToString()
    {
        return $"Text: {Text} | IsSelected: {IsSelected}";
    }
}


public partial class NavBarViewModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<NavBarItemVm> _topItems = [];

    public NavBarViewModel()
    {
        
    }

    public NavBarViewModel(IEnumerable<NavBarItemVm> items)
    {
        _topItems = items;
        SelectedItem = TopItems.First();
    }

    [ObservableProperty]
    private NavBarItemVm _selectedItem;

    partial void OnSelectedItemChanging(NavBarItemVm oldValue, NavBarItemVm newValue)
    {
        Console.WriteLine("-------------");
        Console.WriteLine("OnSelectedItemChanging");
    
        if(oldValue is not null )
        {
            Console.WriteLine($"{oldValue}");
            oldValue.IsSelected = false;
            Console.WriteLine($"{oldValue}");
        }

        if (newValue is null) return;
        Console.WriteLine($"{newValue}");
        newValue.IsSelected = true;
        Console.WriteLine($"{newValue}");
    }
    
    
    partial void OnSelectedItemChanged(NavBarItemVm oldValue, NavBarItemVm newValue)
    {
        Console.WriteLine("-------------");
        Console.WriteLine("OnSelectedItemChanged");
        Console.WriteLine($"{oldValue}");
        Console.WriteLine($"{newValue}");
    }

}