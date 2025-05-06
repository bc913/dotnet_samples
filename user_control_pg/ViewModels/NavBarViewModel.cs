using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bcan.Uctrls.Pg.ViewModels;

public class NavBarItemVm
{
    public NavBarItemVm(string text, bool isSelected) 
    {
        Text = text;
        IsSelected = isSelected;
    }
    public string Text {get; set;} = string.Empty;

    public bool IsSelected { get; set; } = false;

    public override string ToString()
    {
        return $"Text: {Text} | IsSelected: {IsSelected}";
    }

}

public partial class NavBarViewModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<NavBarItemVm> _items = [];

    public NavBarViewModel()
    {
        
    }

    public NavBarViewModel(IEnumerable<NavBarItemVm> items)
    {
        _items = items;
        SelectedItem = Items.First();
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
        
        if(newValue is not null)
        {
            Console.WriteLine($"{newValue}");
            newValue.IsSelected = true;
            Console.WriteLine($"{newValue}");
        }

        

    }

    partial void OnSelectedItemChanged(NavBarItemVm oldValue, NavBarItemVm newValue)
    {
        Console.WriteLine("-------------");
        Console.WriteLine("OnSelectedItemChanged");
        Console.WriteLine($"{oldValue}");
        Console.WriteLine($"{newValue}");
    }

}