using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SimpleWPF.TypeConverters;
using SimpleWPF.ViewModels.Interfaces;

namespace SimpleWPF.Views;

public partial class CrudControl : UserControl
{
    public CrudControl()
    {
        InitializeComponent();
        Loaded += ControlLoadedEventHandler;
    }
    
    private static void AddColumns(ListView listView, IDictionary<string, string> bindings)
    {
        var gridView = new GridView();
        foreach (var (columnName, binding) in bindings)
        {
            gridView.Columns.Add(new GridViewColumn
            {
                Header = columnName,
                DisplayMemberBinding = new Binding($"{binding}")
                {
                    Converter = new ListTagTypeConverter()
                }
            });
        }
        listView.View = gridView;
    }

    private void ControlLoadedEventHandler(object sender, RoutedEventArgs e)
    {
        var dataContext = (IItemListViewModel) DataContext;
        AddColumns(ItemList, dataContext.ColumnsBindings);
    }
}